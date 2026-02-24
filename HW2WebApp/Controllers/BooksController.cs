using Microsoft.AspNetCore.Mvc;
using HW2WebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace HW2WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
    {
        new Book { Id = 1, Title = "1984", Author = "George Orwell", Year = 1949 },
        new Book { Id = 2, Title = "The Hobbit", Author = "J.R.R. Tolkien", Year = 1937 },
        new Book { Id = 3, Title = "Fahrenheit 451", Author = "Ray Bradbury", Year = 1953 }
    };

        [HttpGet]
        public IActionResult GetAll() => Ok(books);

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound("Book not found");
            return Ok(book);
        }
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string title, [FromQuery] string author)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Parameter 'title' is required");

            var results = books
                .Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(author))
                results = results.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

            return Ok(results.ToList());
        }
        [HttpPost]
        public IActionResult Create([FromBody] Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author))
                return BadRequest("Title and Author are required");
            if (book.Year < 1800)
                return BadRequest("Year must be >= 1800");

            book.Id = books.Max(b => b.Id) + 1;
            books.Add(book);

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Book updated)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound("Book not found");

            if (string.IsNullOrWhiteSpace(updated.Title) || string.IsNullOrWhiteSpace(updated.Author))
                return BadRequest("Title and Author are required");
            if (updated.Year < 1800)
                return BadRequest("Year must be >= 1800");

            book.Title = updated.Title;
            book.Author = updated.Author;
            book.Year = updated.Year;

            return Ok(book);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound("Book not found");

            books.Remove(book);
            return NoContent();
        }

    }

}
