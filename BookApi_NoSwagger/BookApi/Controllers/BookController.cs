using BookApi.Models;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BooksService _booksService;
        public BookController(BooksService booksService)
        {
            _booksService = booksService;
        }
        //[HttpGet]
        public Task<string> Get()
        {
            return this.GetBooks();
        }
        //[HttpGet]
        [Route("GetBookDetails/{bookId}")]
        public async Task<BookDetails> GetBookDetails(string bookId)
        {
            return await _booksService.BookDetails(bookId);
        }
        [HttpGet]
        [Route("GetBookDetails")]
        public async Task<List<BookDetails>> GetBookDetails()
        {
            return await _booksService.GetListBookDetails();
        }
        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public async Task<string> GetBooks()
        {
            var books = await _booksService.Get();
            if (books == null)
                return "Invalid Id !!!";
            return JsonConvert.SerializeObject(books);
        }
        [HttpPost]
        public async Task<string> Create(Book book)
        {
            await _booksService.Create(book);
            CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
            return "";
        }
        [HttpPut("{id:length(24)}")]
        public async Task<string> Update(string id, [FromBody] Book bookIn)
        {
            var _book = _booksService.Get(id);
            if (_book == null)
            {
                return "Invalid id!!!";
            }
            return await _booksService.Update(id, bookIn);
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<string> Delete(string id)
        {
            var book = _booksService.Get(id);
            if (book == null)
                return "Invalid id !!!";
            await _booksService.Remove(id);
            return "";
        }
        [HttpDelete]
        public async Task<string> DeleteAll()
        {
            await _booksService.RemoveAll();
            return "";
        }
    }
}
