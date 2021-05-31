using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Lib.Infrastructure.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _ctx;

        public BookRepository(LibraryDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> Add(Book model)
        {
            _ctx.Books.Add(model);

            if (await _ctx.SaveChangesAsync() >= 1)
                return true;
            return false;
        }

        public async Task<bool> Delete(Book model)
        {
            var bookToDelete = _ctx.Books.FirstOrDefault(x => x.Id == model.Id);
            _ctx.Books.Remove(bookToDelete);
            if (await _ctx.SaveChangesAsync() >= 1) return true;
            return false;
        }

        public Book GetBook(string isbn)
        {
            return _ctx.Books.FirstOrDefault(x => x.ISBN == isbn);
        }

        public IQueryable<Book> GetBooks()
        {

            return _ctx.Books.Select(x => x);
        }

        public IQueryable<Book> GetBooksByAuthor(Author author)
        {
            return _ctx.Books.Where(x => x.Author == author).Select(x => x);
        }

        public IQueryable<Book> GetBooksByPublicationYear(DateTime date)
        {
            return _ctx.Books.Where(x => x.PublicationDate.Year == date.Year).Select(x => x);
        }

        public IQueryable<Book> GetBooksByTitle(string title)
        {
            return _ctx.Books.Where(x => x.Title == title).Select(x => x);
        }

        public async Task<bool> Update(Book model)
        {
            var bookToUpdate = _ctx.Books.FirstOrDefault(x => x.Id == model.Id);
            _ctx.Books.Update(bookToUpdate);
            if (await _ctx.SaveChangesAsync() >= 1) return true;
            return false;
        }
    }
}
