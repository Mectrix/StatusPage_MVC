using MongoDB.Driver;
using StatusPage_MVC.Models;

namespace StatusPage_MVC.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext()
        {
            var client = new MongoClient("mongodb://localhost:27017/");
            _database = client.GetDatabase("SP_Database");
        }

        public IMongoCollection<Entity> Entities => _database.GetCollection<Entity>("SP_Entity_Collection");
    }
}
 