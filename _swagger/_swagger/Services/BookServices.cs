using _swagger.DataMongoDB;
using _swagger.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _swagger.Services
{
    public class BookServices : IBookServices
    {
        private readonly IMongoCollection<Book> _books;
        private readonly IMongoCollection<CategoryBook> _category;
        public BookServices(MyDB dbClient)
        {
            _books = dbClient.GetBooksCollection();
            _category = dbClient.GetCategoryBookCollection();
        }

        public async Task<Book> CreateBook(Book book)
        {
            await _books.InsertOneAsync(book);
            return book;
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
        public async Task<DeleteResult> RemoveAll() =>
            await _books.DeleteManyAsync(new BsonDocument());
        public async Task<Book> GetBook(string id)
        {
            return await _books.Find(book => book.Id == id).FirstAsync();
        }
        public async Task<IEnumerable<Book>> GetBooks() =>
            await _books.Find(book => true).ToListAsync();

        public async Task<List<BookFollowCategory>> GetListBookFollowCategorys()
        {
            List<BookFollowCategory> dsResult = new List<BookFollowCategory>();
            foreach (Book book in _books.Find(book => true).ToList())
            {
                var result = new BookFollowCategory
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
            }
            return dsResult;
        }
        public async Task<List<BookFollowCategory>> GetBookFollowCategory(string categoryId)
        {
            IEnumerable<Book> ListBook = await _books.Find(book => book.Category == categoryId).ToListAsync();
            List<BookFollowCategory> result = new List<BookFollowCategory>();
            if (ListBook != null)
            {
                foreach (Book book in ListBook)
                {
                    var r = new BookFollowCategory
                    {
                        Id = book.Id,
                        BookName = book.BookName,
                        Price = book.Price,
                        Author = book.Author
                    };
                    var categoryBook = await _category.Find(c => c.Id == book.Category).FirstOrDefaultAsync();
                    if (categoryBook != null)
                    {
                        r.CategoryName = categoryBook.name;
                        r.CategoryId = categoryBook.Id;
                    }
                    result.Add(r);
                }
                return result;
            }
            return null;
        }
    }
}
