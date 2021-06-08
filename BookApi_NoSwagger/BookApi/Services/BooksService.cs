using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookApi.Services
{
    public class BooksService
    {
        private readonly IMongoCollection<Book> _books;
        private readonly IMongoCollection<CategoryBook> _category;
        public BooksService (IBookstoreDatabaseSettings settings, ICategoryBookDatabaseSettings category)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _books = database.GetCollection<Book>(settings.BooksCollectionName);
            _category = database.GetCollection<CategoryBook>(category.CategoryCollectionName);
        }
        public async Task<IEnumerable<Book>> Get() =>
            await _books.Find(book => true).ToListAsync();
        public async Task<Book> Get(string id) =>
            await _books.Find<Book>(book => book.Id == id).FirstOrDefaultAsync();
        public async Task<Book> Create(Book book)
        {
            await _books.InsertOneAsync(book);
            return book;
        }
        public async Task<List<BookDetails>> GetListBookDetails()
        {
            List<BookDetails> dsResult = new List<BookDetails>();
            foreach (Book book in _books.Find(book => true).ToList())
            {
                var result = new BookDetails
                {
                    Id = book.Id,
                    BookName = book.BookName,
                    Price = book.Price,
                    Author = book.Author
                };
                var categoryBook = await _category.Find(c => c.Id == book.Category).FirstOrDefaultAsync();
                if (categoryBook != null)
                {
                    result.CategoryName = categoryBook.name;
                    result.CategoryId = categoryBook.Id;
                }
                dsResult.Add(result);
                //return result;
            }
            return dsResult;
        }
        public async Task<BookDetails> BookDetails(string bookId)
        {
            var book = await _books.Find(book => book.Id == bookId).FirstOrDefaultAsync();
            if(book != null)
            {
                var result = new BookDetails
                {
                    Id = book.Id,
                    BookName = book.BookName,
                    Price = book.Price,
                    Author = book.Author
                };
                var categoryBook = await _category.Find(c => c.name == book.Category).FirstOrDefaultAsync();
                if(categoryBook != null)
                {
                    result.CategoryName = categoryBook.name;
                    result.CategoryId = categoryBook.Id;
                }
                return result;
            }
            return null;
        }

        public async Task<string> Update(string id, Book bookIn)
        {
            await _books.ReplaceOneAsync(book => book.Id == id, bookIn);
            return "";
        }
        public async Task<DeleteResult> Remove(Book bookIn) =>
            await _books.DeleteOneAsync(book => book.Id == bookIn.Id);
        public async Task<DeleteResult> Remove(string id) =>
            await _books.DeleteOneAsync(book => book.Id == id);
        public async Task<DeleteResult>  RemoveAll() =>
            await _books.DeleteManyAsync(new BsonDocument());
    }
}
