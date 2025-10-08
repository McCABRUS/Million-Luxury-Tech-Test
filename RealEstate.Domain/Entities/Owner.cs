using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Owner
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdOwner { get; set; } = null!;

        [BsonElement("Name")]
        public string Name { get; set; } = null!;

        [BsonElement("Address")]
        public string? Address { get; set; }

        [BsonElement("Photo")]
        public string? Photo { get; set; }

        [BsonElement("Birthday")]
        public DateTime? Birthday { get; set; }
    }
}