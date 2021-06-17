using MongoDB.Bson.Serialization.Attributes;
using System;

namespace _swagger.Models
{
    public class Account
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Faceboook { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
