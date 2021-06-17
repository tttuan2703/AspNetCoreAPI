using _swagger.Models;
using _swagger.Services;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookServices _bookServices;
        public BookController(IBookServices bookServices)
        {
            _bookServices = bookServices;
        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _bookServices.GetBooks().Result;
            //ConnectKafka.KafkaProcedurerHostedService.SendToKafka("Get_Full_Books", JsonConvert.SerializeObject(books));
            return Ok(books);
        }
        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetBook(string id)
        {
            var book = _bookServices.GetBook(id).Result;
            //ConnectKafka.KafkaProcedurerHostedService.SendToKafka("Get_IDBook: "+id,JsonConvert.SerializeObject(book));
            return Ok(book);
        }
        [HttpGet]
        [Route("GetAllBookFollowCategorys")]
        public async Task<List<BookFollowCategory>> GetListBookFollowCategorys()
        {
            var books = await _bookServices.GetListBookFollowCategorys();
            //ConnectKafka.KafkaProcedurerHostedService.SendToKafka("Get_Book_Follow_Full_Categorys", JsonConvert.SerializeObject(books));
            return books;
        }
        [HttpGet]
        [Route("GetBookFollowCategory/{categoryId}")]
        public async Task<List<BookFollowCategory>> GetBookFollowCategory(string categoryId)
        {
            var book = await _bookServices.GetBookFollowCategory(categoryId);
            //ConnectKafka.KafkaProcedurerHostedService.SendToKafka("Get_Book_Follow_Only_Category", JsonConvert.SerializeObject(book));
            return book;
        }
        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            var _book = _bookServices.CreateBook(book);
            if (_book != null)
            {
                ConnectKafka.KafkaProcedurerHostedService.SendToKafka("CreateBook", JsonConvert.SerializeObject(book));
            }
            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(string id)
        {
            _bookServices.Remove(id);
            ConnectKafka.KafkaProcedurerHostedService.SendToKafka("DeleteBook", "Deleted ID Book: "+id);
            return Ok(_bookServices.GetBooks().Result);
        }
        [HttpDelete()]
        public IActionResult DeleteAllBook(string id)
        {
            _bookServices.RemoveAll();
            //ConnectKafka.KafkaProcedurerHostedService.SendToKafka("Delete_All_Books", "Deleted All Books..");
            return Ok(_bookServices.GetBooks().Result);
        }
    }
}
