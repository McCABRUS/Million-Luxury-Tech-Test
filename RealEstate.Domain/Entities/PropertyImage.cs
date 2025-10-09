using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class PropertyImage
    {
        [BsonElement("IdPropertyImage")]
        public string IdPropertyImage { get; set; } = null!;

        [BsonElement("IdProperty")]
        public string IdProperty { get; set; } = null!;

        [BsonElement("File")]
        public string File { get; set; } = null!;

        [BsonElement("Enabled")]
        public bool Enabled { get; set; }
    }
}