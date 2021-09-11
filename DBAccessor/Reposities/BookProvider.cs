using LibraryManagementSystem.DBAccessor.Contracts;
using LibraryManagementSystem.Extensions.JWTConfigure;
using LibraryManagementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DBAccessor.Reposities
{
    public class BookProvider : IBookProvider
    {
        private readonly LMSDbContext _context;
        private IUserService _userService;


        public BookProvider(LMSDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBook(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<(int id, string errMsg)> PutBook(int id, Book book)
        {
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return (id: id, errMsg: "Not Found");
                }
                else
                {
                    throw;
                }
            }

            return (id: id, errMsg: "");
        }

        public async Task<Book> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                throw new Exception("Book not Found");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

    }
}
