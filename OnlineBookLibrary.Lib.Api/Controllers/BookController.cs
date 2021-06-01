using Microsoft.AspNetCore.Mvc;
using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.DTO;
using OnlineBookLibrary.Lib.DTO.AuthorRegister;
using OnlineBookLibrary.Lib.DTO.GenreRequest;
using OnlineBookLibrary.Lib.DTO.PublisherRegister;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;
        private readonly IAuthorRepository _authorRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly IPublisherRepository _publisherRepo;
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
        private readonly IPublisherService _publisherService;
        private readonly IAuthorService _authorService;

        public BookController(IBookRepository bookRepository, IAuthorRepository author, 
            IGenreRepository genre, IPublisherRepository publisher, IBookService bookService, IAuthorService authorService,
            IGenreService genreService, IPublisherService publisherService)
        {
            _bookRepo = bookRepository;
            _authorRepo = author;
            _genreRepo = genre;
            _publisherRepo = publisher;
            _bookService = bookService;
            _genreService = genreService;
            _publisherService = publisherService;
            _authorService = authorService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post([FromBody] BookDetailsDTO model)
        {
            var bookExist = _bookRepo.GetBook(model.ISBN);
            if (bookExist != null) return BadRequest("Book Exist");
            
            var genreExist = _genreRepo.GetGenre(model.Genre.GenreName);
            
            var myGenre = new GenreRegisterDTO
            {
                GenreName = model.Genre.GenreName
            };



            if (genreExist == null)
            {
                genreExist = _genreService.CreateGenre(myGenre);
                if (!await _genreRepo.Add(genreExist)) return StatusCode(500, "An Error Occur, Genre could not be created");
                
            }

            var publisherExist = _publisherRepo.GetPublisher(model.Publisher.PublisherName);

            var myPublisher = new PublisherRegisterDTO
            {
                PublisherName = model.Publisher.PublisherName
            };

            if (publisherExist == null)
            {
                publisherExist = _publisherService.CreatePublisher(myPublisher);
                if(!await _publisherRepo.Add(publisherExist)) return StatusCode(500, "An Error Occur, Publisher could not be created");
            }

            var authorExist = _authorRepo.GetAuthor(model.Author.FirstName, model.Author.LastName);

            var myAuthor = new AuthorRegisterDTO
            {
                FirstName = model.Author.FirstName,
                LastName = model.Author.LastName
            };

            if (authorExist == null)
            {
                authorExist = _authorService.CreateAuthor(myAuthor);
                if(!await _authorRepo.Add(authorExist)) return StatusCode(500, "An Error Occur, Author could not be created");
            }


            var book = new Book
            {
                Title = model.Title,
                Genre = genreExist,
                Language = model.Language,
                Photo = model.Photo,
                Publisher = publisherExist,
                PublicationDate = DateTime.Now,
                ISBN = model.ISBN,
                DateAddedToLibrary = DateTime.Now,
                Author = authorExist,
                Pages = model.Pages,
                Description = model.Description,
                Rating = model.Rating,
                
            };

            if(!await _bookRepo.Add(book)) return StatusCode(500, "An Error Occur, Book could not be created"); ;

            return Ok("Book Successfully Registered");

        }
        
    }
}
