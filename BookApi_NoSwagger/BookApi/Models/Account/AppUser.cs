using MongoDB.Bson.Serialization.Attributes;

namespace BookApi.Models.Account
{
    public class AppUser
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }
    }
}
