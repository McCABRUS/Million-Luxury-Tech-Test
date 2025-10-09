using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Property
    {
        [BsonElement("IdProperty")]
        public string IdProperty { get; set; } = null!;

        [BsonElement("Name")]
        public string Name { get; set; } = null!;

        [BsonElement("Address")]
        public string? Address { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }

        [BsonElement("CodeInternal")]
        public string? CodeInternal { get; set; }

        [BsonElement("Year")]
        public int? Year { get; set; }

        [BsonElement("IdOwner")]
        public string IdOwner { get; set; } = null!;

        [BsonElement("Images")]
        public List<PropertyImage>? Images { get; set; }

        [BsonElement("Traces")]
        public List<PropertyTrace>? Traces { get; set; }
    }
}