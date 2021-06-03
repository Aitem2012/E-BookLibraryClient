using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.DTO;
using OnlineBookLibrary.Lib.DTO.AuthorRegister;
using OnlineBookLibrary.Lib.DTO.BookResponse;
using OnlineBookLibrary.Lib.DTO.GenreRequest;
using OnlineBookLibrary.Lib.DTO.PublisherRegister;
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
        private readonly IGenreService _genreService;
        private readonly IPublisherRepository _publisherRepo;
        private readonly IPublisherService _publisherService;
        private readonly IAuthorService _authorService;
        private readonly ICloudinaryService _cloudinary;
        private readonly IReviewRepository _review;

        public BookService(IBookRepository book, IAuthorRepository author, IGenreRepository genre, IPublisherRepository publisher,
            IGenreService genreService, IPublisherRepository publisherRepository, IPublisherService publisherService,
            IAuthorService authorService, ICloudinaryService cloudinary, IReviewRepository review)
        {
            _review = review;
            _bookRepo = book;
            _authorRepo = author;
            _genreRepo = genre;
            _publisher = publisher;
            _genreService = genreService;
            _publisherRepo = publisherRepository;
            _publisherService = publisherService;
            _authorService = authorService;
            _cloudinary = cloudinary;
           
        }

        public Book CreateBook(BookDetailsDTO model)
        {
            //PhotoUpdateDTO photoToAdd = new PhotoUpdateDTO
            //{
            //    PhotoUrl = model.Photo
            //};
            
            //var photo = await _cloudinary.AddPatchPhoto(photoToAdd);
            return new Book
            {
                Title = model.Title,
                GenreId = model.Genre.Id,
                Language = model.Language,
                Photo = model.PhotoUrl,
                PublisherId = model.Publisher.Id,
                PublicationDate = model.PublicationDate,
                ISBN = model.ISBN,
                DateAddedToLibrary = model.DateAddedToLibrary,
                AuthorId = model.Author.Id,
                Pages = model.Pages,
                Description = model.Description,                
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
                //item.               
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
                    Photo = item.Photo,
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
            var reviews = _review.GetReviewsByBookId(book.Id);
            int Rate = 0;
            foreach(var review in reviews)
            {
                Rate += review.Ratings;
            }

            return new BookResponseDTO
            {
                Title = book.Title,
                ISBN = book.ISBN,
                PublisherName = publisher.PublisherName,
                GenreName = genre.GenreName,
                Photo = book.Photo,
                Language = book.Language,
                Pages = book.Pages,
                Description = book.Description,
                Rating = Rate,
                AuthorName = $"{author.FirstName} {author.LastName}"
            };            
        }

        public async Task<Book> RegisterBook (BookDetailsDTO model)
        {

            var bookExist = _bookRepo.GetBook(model.ISBN);
            if (bookExist != null) return null;

            var genreExist = _genreService.GetGenreByName(model.Genre.GenreName);

            var myGenre = new GenreRegisterDTO
            {
                GenreName = model.Genre.GenreName
            };

            if (genreExist == null)
            {
                genreExist = _genreService.CreateGenre(myGenre);
                if (!await _genreRepo.Add(genreExist)) throw new InvalidOperationException("Genre Could not be created");
            }

            var publisherExist = _publisherService.GetPublisherByName(model.Publisher.PublisherName);

            var myPublisher = new PublisherRegisterDTO
            {
                PublisherName = model.Publisher.PublisherName
            };

            if (publisherExist == null)
            {
                publisherExist = _publisherService.CreatePublisher(myPublisher);
                if (!await _publisherRepo.Add(publisherExist)) throw new InvalidOperationException("Publisher Could not be Created");
            }

            var authorExist = _authorService.GetAuthorByName(model.Author.FirstName, model.Author.LastName);

            var myAuthor = new AuthorRegisterDTO
            {
                FirstName = model.Author.FirstName,
                LastName = model.Author.LastName
            };

            if (authorExist == null)
            {
                authorExist = _authorService.CreateAuthor(myAuthor);
                if (!await _authorRepo.Add(authorExist)) throw new InvalidOperationException("Author Could not be created");
            }

            model.Genre = genreExist;
            model.Author = authorExist;
            model.Publisher = publisherExist;



            var book = CreateBook(model);


            return book;
        }

        public BookResponseDTO GetBookByTitle(string title)
        {
            var books = _bookRepo.GetBooks();

            var book = books.FirstOrDefault(x => x.Title == title);
            
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
                
                AuthorName = $"{author.FirstName} {author.LastName}"
            }
            ;
        }

        public async Task<bool> Add(Book book)
        {
            if (!await _bookRepo.Add(book))
            {
                return false;
            }
            return true;
        }
    }
}
