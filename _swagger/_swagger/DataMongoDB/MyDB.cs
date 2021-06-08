using _swagger.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _swagger.DataMongoDB
{
    public class MyDB
    {
        private MongoClient mongoClient { get; }
        private IMongoDatabase Database { get; }
        public MyDB(string connectionString, string dbName)
        {
            mongoClient = new MongoClient(connectionString);
            Database = mongoClient.GetDatabase(dbName);
        }
        public IMongoCollection<Book> GetBooksCollection() {
            return Database.GetCollection<Book>("Books");
        }
    }
}
