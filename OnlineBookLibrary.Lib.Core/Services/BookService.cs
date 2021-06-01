using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.DTO;
using OnlineBookLibrary.Lib.DTO.BookResponse;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly IAuthorRepository _authorRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly IPublisherRepository _publisher;

        public BookService(IBookRepository book, IAuthorRepository author, IGenreRepository genre, IPublisherRepository publisher)
        {
            _bookRepo = book;
            _authorRepo = author;
            _genreRepo = genre;
            _publisher = publisher;
        }
        public Book CreateBook(BookDetailsDTO model)
        {

            return new Book
            {
                Title = model.Title,
                GenreId = model.Genre.Id,
                Language = model.Language,
                Photo = model.Photo,
                PublisherId = model.Publisher.Id,
                PublicationDate = model.PublicationDate,
                ISBN = model.ISBN,
                DateAddedToLibrary = model.DateAddedToLibrary,
                AuthorId = model.Author.Id,
                Pages = model.Pages,
                Description = model.Description,
                Rating = model.Rating
            };
        }

        public IEnumerable<BookResponseDTO> GetBooksByAuthor(int id)
        {
            var author = _authorRepo.GetAuthor(id);
            if (author == null) return null;

            var book = _bookRepo.GetBooksByAuthor(author).ToList();
            var bookList = new List<Book>();
            var books = new List<BookResponseDTO>();
            foreach (var item in book)
            {
                bookList.Add(item);
            }

            foreach (var item in bookList)
            {
                var bookGenre = _genreRepo.GetGenre(item.GenreId);
                var publisher = _publisher.GetPublisher(item.PublisherId);
                books.Add(new BookResponseDTO
                {
                    ISBN = item.ISBN,
                    Title = item.Title,
                    AuthorName = $"{author.FirstName} {author.LastName}",
                    Language = item.Language,
                    Pages = item.Pages,
                    GenreName = bookGenre.GenreName,
                    Description = item.Description,
                    Rating = item.Rating,
                    PublisherName = publisher.PublisherName
                });
            }

            return books;


        }

        public IEnumerable<BookResponseDTO> GetAllBooks()
        {
            var books = _bookRepo.GetBooks().ToList();

            if (books.Count <= 0) return null;

            var bookList = new List<BookResponseDTO>();
            

            foreach (var item in books)
            {
                var bookGenre = _genreRepo.GetGenre(item.GenreId);
                var publisher = _publisher.GetPublisher(item.PublisherId);
                var author = _authorRepo.GetAuthor(item.AuthorId);
                bookList.Add(new BookResponseDTO
                {
                    ISBN = item.ISBN,
                    Title = item.Title,
                    AuthorName = $"{author.FirstName} {author.LastName}",
                    Language = item.Language,
                    Pages = item.Pages,
                    GenreName = bookGenre.GenreName,
                    Description = item.Description,
                    Rating = item.Rating,
                    PublisherName = publisher.PublisherName
                });
            }

            return bookList;

        }
        public BookResponseDTO GetBookByISBN (string isbn)
        {
            
            var book = _bookRepo.GetBook(isbn);
            if (book == null)
            {
                return null;
            }
            var author = _authorRepo.GetAuthor(book.AuthorId);
            var publisher = _publisher.GetPublisher(book.PublisherId);
            var genre = _genreRepo.GetGenre(book.GenreId);

            

            return new BookResponseDTO
            {
                Title = book.Title,
                ISBN = book.ISBN,
                PublisherName = publisher.PublisherName,
                GenreName = genre.GenreName,
                Language = book.Language,
                Pages = book.Pages,
                Rating = book.Rating,
                AuthorName = $"{author.FirstName} {author.LastName}"
            }
            ;
        }
    }
}
