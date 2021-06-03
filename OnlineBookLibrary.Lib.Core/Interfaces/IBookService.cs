using OnlineBookLibrary.Lib.DTO;
using OnlineBookLibrary.Lib.DTO.BookResponse;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Interfaces
{
    public interface IBookService
    {
        public Book CreateBook(BookDetailsDTO model);
        public IEnumerable<BookResponseDTO> GetBooksByAuthor(int id);
        public BookResponseDTO GetBookByISBN(string isbn);
        public IEnumerable<BookResponseDTO> GetAllBooks();
        public BookResponseDTO GetBookByTitle(string title);
        Task<Book> RegisterBook(BookDetailsDTO model);
        Task<bool> Add(Book book);
    }
}
