using _swagger.Models;
using MongoDB.Driver;
using System;

namespace _swagger.DataMongoDB
{
    public class MyDB
    {
        private MongoClient mongoClient { get; }
        private IMongoDatabase Database { get; }
        public MyDB(string connectionString, string dbName)
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(connectionString, 27017);
            mongoClient = new MongoClient(settings);
            Database = mongoClient.GetDatabase(dbName);

            /*var url = new MongoUrl(connectionString);
            mongoClient = new MongoClient(url);
            Database = mongoClient.GetDatabase(dbName);*/
        }
        public IMongoCollection<Book> GetBooksCollection()
        {
            return Database.GetCollection<Book>("Books");
        }
        public IMongoCollection<Account> GetAccountsCollection()
        {
            var accounts = Database.GetCollection<Account>("Accounts");
            return accounts;
        }
        public IMongoCollection<CategoryBook> GetCategoryBookCollection()
        {
            return Database.GetCollection<CategoryBook>("CategoryBook");
        }
    }
}
