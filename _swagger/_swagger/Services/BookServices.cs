using _swagger.DataMongoDB;
using _swagger.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _swagger.Services
{
    public class BookServices : IBookServices
    {
        private readonly IMongoCollection<Book> _book;
        public BookServices(MyDB dbClient)
        {
            _book = dbClient.GetBooksCollection();
        }

        public async Task<Book> CreateBook(Book book)
        {
            await _book.InsertOneAsync(book);
            return book;
        }
        public async Task<string> Update(string id, Book bookIn)
        {
            await _book.ReplaceOneAsync(book => book.Id == id, bookIn);
            return "";
        }
        public async Task<DeleteResult> Remove(Book bookIn) =>
            await _book.DeleteOneAsync(book => book.Id == bookIn.Id);
        public async Task<DeleteResult> Remove(string id) =>
            await _book.DeleteOneAsync(book => book.Id == id);
        public async Task<DeleteResult> RemoveAll() =>
            await _book.DeleteManyAsync(new BsonDocument());
        public async Task<Book> GetBook(string id)
        {
            return await _book.Find(book => book.Id == id).FirstAsync();
        }
        public async Task<IEnumerable<Book>> GetBooks()=>
            await _book.Find(book => true).ToListAsync();

        
    }
}
