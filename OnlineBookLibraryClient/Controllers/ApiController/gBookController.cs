﻿using Microsoft.AspNetCore.Mvc;
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
    public class gBookController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;
        private readonly IAuthorRepository _authorRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly IPublisherRepository _publisherRepo;
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
        private readonly IPublisherService _publisherService;
        private readonly IAuthorService _authorService;

        public gBookController(IBookRepository bookRepository, IAuthorRepository author,
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

            var genreExist = _genreRepo.GetGenre(model.Genre.Id);

            var myGenre = new GenreRegisterDTO
            {
                GenreName = model.Genre.GenreName
            };



            if (genreExist == null)
            {
                genreExist = _genreService.CreateGenre(myGenre);
                if (!await _genreRepo.Add(genreExist)) return StatusCode(500, "An Error Occur, Genre could not be created");

            }

            var publisherExist = _publisherRepo.GetPublisher(model.Publisher.Id);

            var myPublisher = new PublisherRegisterDTO
            {
                PublisherName = model.Publisher.PublisherName
            };

            if (publisherExist == null)
            {
                publisherExist = _publisherService.CreatePublisher(myPublisher);
                if (!await _publisherRepo.Add(publisherExist)) return StatusCode(500, "An Error Occur, Publisher could not be created");
            }

            var authorExist = _authorRepo.GetAuthor(model.Author.Id);

            var myAuthor = new AuthorRegisterDTO
            {
                FirstName = model.Author.FirstName,
                LastName = model.Author.LastName
            };

            if (authorExist == null)
            {
                authorExist = _authorService.CreateAuthor(myAuthor);
                if (!await _authorRepo.Add(authorExist)) return StatusCode(500, "An Error Occur, Author could not be created");
            }

            model.Genre = genreExist;
            model.Author = authorExist;
            model.Publisher = publisherExist;



            var book = await _bookService.CreateBook(model);

            if (!await _bookRepo.Add(book)) return StatusCode(500, "An Error Occur, Book could not be created"); ;

            return Ok("Book Successfully Registered");

        }

        [HttpGet]
        [Route("all-books")]
        public IActionResult Get()
        {
            var books = _bookService.GetAllBooks();
            if (books == null) return NotFound("No book found");
            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {

            var books = _bookService.GetBooksByAuthor(id).ToList();
            if (books == null) return NotFound("No Such Author");
            if (books.Count <= 0) return BadRequest("Author Does Not have Any book");
            return Ok(books);
        }

        [HttpGet]
        [Route("get-book-by/{isbn}")]
        public IActionResult Get(string isbn)
        {
            var book = _bookService.GetBookByISBN(isbn);
            if (book == null) return NotFound("Book Not Found");
            
            return Ok(book);
        }
    }
}
