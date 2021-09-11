using LibraryManagementSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.ContractsBLL
{
   public interface IBookBLL
    {
        public Task<IEnumerable<Book>> GetBooks();
        public Task<Book> GetBook(int id);
        public Task<(int id, string errMsg)> PutBook(int id, Book book);

        public Task<Book> PostBook(Book book);
        public Task DeleteBook(int id);
    }
}
