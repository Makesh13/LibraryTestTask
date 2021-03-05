using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Library.Resource.Api.Domain;
using Microsoft.AspNetCore.Authorization;

//using Library.Resource.Api.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Resource.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly EFBooks books;
        public BooksController(EFBooks books)
        {
            this.books = books;
        }
        // GET: api/<BooksController> localhost/api/book/
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(books.GetBooks());
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(books.GetBook(id));
        }

        // POST api/<BooksController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public void Post([FromBody] Book book)
        {
            books.SaveBook(book);
        }

        /*// PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] Book book)
        {
            books.SaveBook(book);
        }*/

        // DELETE api/<BooksController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            books.Delete(id);
        }
    }
}
