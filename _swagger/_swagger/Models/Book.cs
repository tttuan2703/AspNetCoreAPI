using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _swagger.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]//Nhằm để sử dụng dạng string nhưng MongoDB vẫn hiểu được
        public string Id { get; set; }
        [BsonElement("Name")]//Do trong MongoDB tên field là Name
        public string BookName { get; set; }
        public decimal Price { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Category { get; set; }
        public string Author { get; set; }
    }
}
