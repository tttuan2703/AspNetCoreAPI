using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _swagger.Models
{
    public class CategoryBook
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string name { get; set; }
        public double number { get; set; }
    }
}
