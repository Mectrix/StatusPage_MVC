using MongoDB.Driver;
using StatusPage_MVC.Models;

namespace StatusPage_MVC.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext()
        {

            //var client = new MongoClient("mongodb://localhost:27017/");

            var client = new MongoClient("mongodb+srv://mectrix:FVT3F8Zmd5KEpW9I@mectrixmongo.9ibzsqt.mongodb.net/");
            _database = client.GetDatabase("SPX3");
        }

        public IMongoCollection<Entity> Entities => _database.GetCollection<Entity>("sp_entities");
    }
}
