using LibraryManagementSystem.BLL.ContractsBLL;
using LibraryManagementSystem.DBAccessor.Contracts;
using LibraryManagementSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.RepositoriesBLL
{
    public class BookBLL : IBookBLL
    {
        private IBookProvider _bookProvider;

        public BookBLL(IBookProvider bookProvider)
        {
            _bookProvider = bookProvider;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookProvider.GetBooks();
        }

        public async Task<Book> GetBook(int id)
        {
            return await _bookProvider.GetBook(id);
        }
        public async Task<(int id, string errMsg)> PutBook(int id, Book book)
        {
            return await _bookProvider.PutBook(id, book);
        }

        public async Task<Book> PostBook(Book book)
        {
            return await _bookProvider.PostBook(book);
        }

        public async Task DeleteBook(int id)
        {
            await _bookProvider.DeleteBook(id);
        }
    }
}
