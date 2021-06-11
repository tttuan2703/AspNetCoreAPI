using MongoDB.Driver;

namespace ConsoleApp_DockerWithKafka.MongoDBConnection
{
    public class bookStoreDB
    {
        private MongoClient _client { get; set; }
        public IMongoDatabase _database { get; set; }
        public bookStoreDB()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("bookStoreDb");
        }
    }
}
