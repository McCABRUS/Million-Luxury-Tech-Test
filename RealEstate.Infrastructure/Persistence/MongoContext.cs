using MongoDB.Driver;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Persistence
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;
        public MongoContext(IMongoDatabase database) => _database = database;
        public IMongoCollection<Property> Properties => _database.GetCollection<Property>("properties");
        public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("owners");
        public IMongoCollection<PropertyImage> PropertyImages => _database.GetCollection<PropertyImage>("propertyImages");
        public IMongoCollection<PropertyTrace> PropertyTraces => _database.GetCollection<PropertyTrace>("propertyTraces");
    }
}

