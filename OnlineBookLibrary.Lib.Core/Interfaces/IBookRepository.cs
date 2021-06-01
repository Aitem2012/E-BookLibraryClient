using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Interfaces
{
    public interface IBookRepository : ICRUDRepository<Book>
    {
        public Book GetBook(string isbn);
        public IQueryable<Book> GetBooks();
        public IQueryable<Book> GetBooksByTitle(string title);
        public IQueryable<Book> GetBooksByAuthor(Author author);
        public IQueryable<Book> GetBooksByPublicationYear(DateTime date);
    }
}
