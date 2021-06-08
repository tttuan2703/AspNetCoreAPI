using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using _swagger.Services;
using _swagger.Models;

using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookServices _bookServices;
        public BookController(IBookServices bookServices) {
            _bookServices = bookServices;
        }
        [HttpGet]
        public IActionResult GetBook() {
            return Ok(_bookServices.GetBooks().Result);
        }
        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetBook(string id)
        {
            return Ok(_bookServices.GetBook(id).Result);
        }
        [HttpPost]
        public IActionResult CreateBook(Book book){
            _bookServices.CreateBook(book);
            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(string id)
        {
            _bookServices.Remove(id);
            return Ok(_bookServices.GetBooks().Result);
        }
        [HttpDelete()]
        public IActionResult DeleteAllBook(string id)
        {
            _bookServices.RemoveAll();
            return Ok(_bookServices.GetBooks().Result);
        }
    }
}
