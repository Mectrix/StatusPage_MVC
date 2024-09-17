using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StatusPage_MVC.Models
{
    public class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public required string Name { get; set; }
        public string? CMDB { get; set; }
        public string? Type { get; set; }

        public DateTime LastChecked { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<ObjectId>? Dependencies { get; set; } = new List<ObjectId>();

        public List<Kpi>? Kpis { get; set; } = new List<Kpi>();
        public List<Sli>? Slis { get; set; } = new List<Sli>();
        public List<Slo>? Slos { get; set; } = new List<Slo>();
    }

    public class Kpi
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? Source { get; set; }
        public string? DeepLink { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime LastChecked { get; set; }
        public string? Status { get; set; }
    }

    public class Sli
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Name { get; set; }
        public string? Value { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class Slo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Name { get; set; }
        public string? Target { get; set; }
        public string? DegradedTotal { get; set; }
        public string? OutageTotal { get; set; }
        public string? UnavailableTotal { get; set; }
        public DateTime Timestamp { get; set; }
    }


    public class AddKpiViewModel
    {
        public Kpi? Kpi { get; set; }
        public List<Entity>? Entities { get; set; }
        public string? SelectedEntityId { get; set; }
    }


    //public class AddDependencyViewModel
    //{
    //    public List<Entity>? Entities { get; set; }
    //    public ObjectId Dependency { get; set; }
    //    public string? SelectedEntityId { get; set; }
    //}

    //public class AddDependencyViewModel
    //{
    //    public string? EntityId { get; set; }
    //    public string? DependencyId { get; set; }
    //}

    public class AddDependencyViewModel
    {
        public string? EntityId { get; set; }
        public string? DependencyId { get; set; }
        public List<SelectListItem>? Entities { get; set; }
    }


}
