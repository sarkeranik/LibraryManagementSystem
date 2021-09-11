using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.DBAccessor;
using LibraryManagementSystem.Models.Entities;
using LibraryManagementSystem.Extensions.JWTConfigure;
using Microsoft.AspNetCore.Authorization;
using AuthorizeAttribute = LibraryManagementSystem.Extensions.JWTConfigure.AuthorizeAttribute;
using LibraryManagementSystem.BLL.ContractsBLL;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LMSDbContext _context;
        private IUserService _userService;
        private IBookBLL _bookBLL;

        public BooksController(LMSDbContext context, IUserService userService, IBookBLL bookBLL)
        {
            _context = context;
            _userService = userService;
            _bookBLL = bookBLL;
        }


        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            try
            {
                var response = _userService.Authenticate(model);

                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            try
            {
                return Ok(await _bookBLL.GetBooks());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            try
            {
                var book = await _bookBLL.GetBook(id);

                if (book == null)
                {
                    return NotFound();
                }

                return book;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Books/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            try
            {
                if (id != book.Id)
                {
                    return BadRequest();
                }
                var res = await _bookBLL.PutBook(id, book);

                if (string.IsNullOrEmpty(res.errMsg))
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { id = id, book = book });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // POST: api/Books
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            try
            {
                await _bookBLL.PostBook(book);

                return CreatedAtAction("GetBook", new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Books/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookBLL.DeleteBook(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Book not Found"))
                {
                    return NotFound(id);
                }

                return BadRequest(ex.Message);

            }
        }

    }
}
