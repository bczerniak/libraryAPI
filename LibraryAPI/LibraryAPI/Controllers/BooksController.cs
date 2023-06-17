using LibraryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookContext _context;

        public BooksController(BookContext context)
        {
            _context = context;
        }

        [Route("api/allbooks")]
        [HttpGet]
        public IEnumerable<Book> GetAllBooksAscendingByTitle()
        {
            return _context.Books.OrderBy(b => b.Title).ToList();
        }

        [Route("api/book/id/{id}")]
        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var Book = _context.Books.Find(id);

            if (Book == null)
            {
                return NotFound();
            }

            return Book;
        }

        [Route("api/book/title/{title}")]
        [HttpGet("{title}")]
        public ActionResult<Book> GetBookByTitle(string title)
        {
            var Book = _context.Books.FirstOrDefault(b => b.Title == title);

            if (Book == null)
            {
                return NotFound();
            }

            return Book;
        }

        [Route("api/addbook")]
        [HttpPost]
        public ActionResult<Book> AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [Route("api/updatebook/{id}")]
        [HttpPut("{id}")]
        public ActionResult<Book> UpdateBook(int id, Book book)
        {
            if(id != book.Id)
            {
                return BadRequest();
            }

            _context.Books.Update(book);
            _context.SaveChanges();

            return NoContent();
        }

        [Route("api/deletebook/{id}")]
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            var Book = _context.Books.Find(id);

            if (Book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(Book);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
