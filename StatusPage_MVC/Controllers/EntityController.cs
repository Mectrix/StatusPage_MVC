using System;
using System.Collections.Generic;
 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Driver;
using StatusPage_MVC.Data;
using StatusPage_MVC.Models;

namespace StatusPage_MVC.Controllers
{

    public class EntityController : Controller
    {
        private readonly MongoDBContext _context;

        public EntityController()
        {
            _context = new MongoDBContext();
        }

        public ActionResult Index()
        {
            var entities = _context.Entities.Find(new BsonDocument()).ToList();
            return View(entities);
        }



        public async Task<IActionResult> List()
        {
            var entities = await _context.Entities.Find(_ => true).ToListAsync();
            return View(entities);
        }









        public ActionResult Details(string id)
        {
            var entity = _context.Entities.Find(e => e.Id == id).FirstOrDefault();
            return View(entity);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Entity entity)
        {
            if (ModelState.IsValid)
            {
                entity.Id = ObjectId.GenerateNewId().ToString();
                entity.LastChecked = DateTime.Now;
                _context.Entities.InsertOne(entity);
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public ActionResult Edit(string id)
        {
            var entity = _context.Entities.Find(e => e.Id == id).FirstOrDefault();
            return View(entity);
        }

        [HttpPost]
        public ActionResult Edit(Entity entity)
        {
            if (ModelState.IsValid)
            {
                _context.Entities.ReplaceOne(e => e.Id == entity.Id, entity);
                return RedirectToAction("Index");
            }
            return View(entity);
        }



        // GET: Entity/AddKpi
        public IActionResult AddKpi()
        {
            var entities = _context.Entities.Find(entity => true).ToList();
            var viewModel = new AddKpiViewModel
            {
                Entities = entities,
                Kpi = new Kpi()
            };
            return View(viewModel);
        }

        // POST: Entity/AddKpi
        [HttpPost]
        public IActionResult AddKpi(AddKpiViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Kpi kpi = new Kpi
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = viewModel.Kpi.Name,
                    Value = viewModel.Kpi.Value,
                    Source = viewModel.Kpi.Source,
                    DeepLink = viewModel.Kpi.DeepLink,
                    Timestamp = DateTime.Now,
                    LastChecked = DateTime.Now,
                    Status = viewModel.Kpi.Status
                };
                viewModel.Kpi = kpi;   // Update the Kpi object in the view model
                var filter = Builders<Entity>.Filter.Eq(e => e.Id, viewModel.SelectedEntityId);
                var update = Builders<Entity>.Update.Push(e => e.Kpis, viewModel.Kpi);
                // Debugging: Inspect the Kpi object
                _context.Entities.UpdateOne(filter, update);
                return RedirectToAction("Index");
            }
            viewModel.Entities = _context.Entities.Find(entity => true).ToList();
            return View(viewModel);
        }



        // GET: Entity/AddDependency
        public IActionResult AddDependency()
        {
            var entities = _context.Entities.Find(entity => true).ToList();
            var entityList = new List<SelectListItem>();

            foreach (var entity in entities)
            {
                entityList.Add(new SelectListItem { Value = entity.Id, Text = entity.Name });
            }

            var viewModel = new AddDependencyViewModel
            {
                Entities = entityList
            };

            return View(viewModel);
        }

        // POST: Entity/AddDependency
        [HttpPost]
        public IActionResult AddDependency(AddDependencyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var filter = Builders<Entity>.Filter.Eq(e => e.Id, viewModel.EntityId);
                var update = Builders<Entity>.Update.Push(e => e.Dependencies, new ObjectId(viewModel.DependencyId));
                var result = _context.Entities.UpdateOne(filter, update);

                if (result.ModifiedCount > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Entity not found or no changes made.");
                }
            }

            var entities = _context.Entities.Find(entity => true).ToList();
            viewModel.Entities = new List<SelectListItem>();

            foreach (var entity in entities)
            {
                viewModel.Entities.Add(new SelectListItem { Value = entity.Id, Text = entity.Name });
            }

            return View(viewModel);
        }


        public ActionResult VisualizeDependencies()
        {
            var entities = _context.Entities.Find(new BsonDocument()).ToList();
            return View(entities);
        }

        [HttpGet]
        public JsonResult GetEntityWithDependencies(string id)
        {
            var entity = _context.Entities.Find(e => e.Id == id).FirstOrDefault();
            return Json(entity);
        }




        public ActionResult Delete(string id)
        {
            var entity = _context.Entities.Find(e => e.Id == id).FirstOrDefault();
            return View(entity);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            _context.Entities.DeleteOne(e => e.Id == id);
            return RedirectToAction("Index");
        }
    }


}