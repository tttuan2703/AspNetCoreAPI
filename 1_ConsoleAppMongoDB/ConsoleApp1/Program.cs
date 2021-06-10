using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var client = new MongoClient("mongodb+srv://admin:pass123@cluster0.pycf2.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");

            //Connect Alats MongoDb
            //var database = client.ListDatabases().ToList();
            //Console.WriteLine("The list of databases on this server is: ");

            //foreach(var db in database)
            //{
            //    Console.WriteLine(db);
            //}

            //Get Collection
            var database = client.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");
            //Create document local
            //var document = new BsonDocument { { "student_id", 10000 }, { "scores",
            //        new BsonArray
            //        {
            //            new BsonDocument{{"type","exam"},{"score",88.121218281482828}},
            //            new BsonDocument{{"type","quiz"},{"score",88.121218281482828}},
            //            new BsonDocument{{"type","homework"},{"score",88.121218281482828}},
            //            new BsonDocument{{"type","homework"},{"score",88.121218281482828}}
            //        }
            //    },{"class_id",480}
            //};


            //Console.WriteLine(collection.ToJson().ToString());
            ////Check result
            //foreach (object obj in document)
            //{
            //    Console.WriteLine(obj);
            //}


            ////Read Document in MongoDB
            //var firstDocument = collection.Find(new BsonDocument()).FirstOrDefault();
            //Console.WriteLine(firstDocument.ToString());

            ////Create Document for Collection
            //collection.InsertOne(document);

            ////Read with a Filter
            //var filter = Builders<BsonDocument>.Filter.Eq("student_id", 10000);
            //var studentDocument = collection.Find(filter).FirstOrDefault();
            //Console.WriteLine(studentDocument.ToString());

            ////Read All Document 
            //var documents = collection.Find(new BsonDocument()).ToList();
            //foreach (BsonDocument doc in documents)
            //{
            //    Console.WriteLine(doc.ToJson().ToString());
            //}

            //Read Document be selective
            //var highFilter = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>(
            //    "scores", new BsonDocument { { "type", "exam" } ,{
            //    "score", new BsonDocument { { "$gte", 95 } } } }
            //    );
            //var highScore = collection.Find(highFilter).ToList();
            //foreach (Object obj in highScore)
            //{
            //    Console.WriteLine(obj);
            //}
            //Or 
            //var cusor = collection.Find(highFilter).ToCursor();
            //foreach (var _document in cusor.ToEnumerable())
            //{
            //    Console.WriteLine(document);
            //}
            //Or asynchronously 
            //await collection.Find(highFilter).ForEachAsync(document => Console.WriteLine(document));

            //Sorting
            //var _sort = Builders<BsonDocument>.Sort.Descending("student_id");
            //var highestScores = collection.Find(highFilter).Sort(_sort);
            ////Append the First()
            //Console.WriteLine(highestScores.First());

            //Update Filter
            //var _filter = Builders<BsonDocument>.Filter.Eq("student_id", 10000);
            //var _update = Builders<BsonDocument>.Update.Set("class_id", 483);
            //collection.UpdateOne(_filter, _update);
            //Console.WriteLine(collection.Find(_filter).ToList().ToJson().ToString());

            ////Array Changes
            //var arrayFillter = Builders<BsonDocument>.Filter.Eq("student_id", 1000)
            //& Builders<BsonDocument>.Filter.Eq("scores.type", "quiz");
            //var arrayUpdate = Builders<BsonDocument>.Update.Set("scores.$.score", 80);
            //collection.UpdateMany(arrayFillter, arrayUpdate);
            //Console.WriteLine(collection.Find(arrayFillter).ToList().ToJson().ToString());

            //Deleting one Data
            var _deleterFilter = Builders<BsonDocument>.Filter.Eq("student_id", 10000);
            collection.DeleteOne(_deleterFilter);
            //Show result
            Console.WriteLine(collection.Find(Builders<BsonDocument>.Filter.Eq("student_id", 10000)).ToList().ToJson().ToString());

            //Multi delete
            var deleteLowExam = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>("scores", new BsonDocument
            { { "type","exam"},{"score",new BsonDocument{{"$lt",60 } } } });
            collection.DeleteMany(deleteLowExam);

            //Check result
            Console.WriteLine(collection.Find(Builders<BsonDocument>.Filter.Eq("scores", new BsonDocument
            { { "type","exam"},{"score",new BsonDocument{{"$lt",60 } } } })).ToList().ToJson().ToString());

            Console.ReadLine();
        }
    }
    public class Book
    {
        public ObjectId _id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Published { get; set; }
        public ObjectId Publisher { get; set; }
        public Imageurl[] imageURL { get; set; }
        public Rating[] rating { get; set; }
    }

    public class Imageurl
    {
        public int Size { get; set; }
        public string Url { get; set; }
    }

    public class Rating
    {
        public ObjectId userId { get; set; }
        public int value { get; set; }
    }
}
