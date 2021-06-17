using _swagger.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _swagger.Services
{
    public interface IBookServices
    {
        public Task<Book> CreateBook(Book book);
        public Task<string> Update(string id, Book bookIn);
        public Task<DeleteResult> Remove(Book bookIn);
        public Task<DeleteResult> Remove(string id);
        public Task<DeleteResult> RemoveAll();
        public Task<Book> GetBook(string id);
        public Task<IEnumerable<Book>> GetBooks();
        public Task<List<BookFollowCategory>> GetListBookFollowCategorys();
        public Task<List<BookFollowCategory>> GetBookFollowCategory(string categoryId);
    }
}
