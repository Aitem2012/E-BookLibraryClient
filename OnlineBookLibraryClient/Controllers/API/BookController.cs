using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.DTO;
using OnlineBookLibrary.Lib.DTO.Pagination;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;
        
        private readonly IBookService _bookService;

        //Book Controller Constructor
        public BookController(IBookRepository bookRepository, IMapper mapper, IAuthorService authorService, IBookService bookService
            )
        {
            _bookRepo = bookRepository;
            _mapper = mapper;
           
            _bookService = bookService;
        }

        /// <summary>
        /// Gets Paginated List of Books
        /// </summary>
        [HttpGet]
        public IActionResult GetBooks([FromQuery] PagingParameters model)
        {
            try
            {
                var books = _bookRepo.GetBooks().ToList();
                if (books.Count <= 0)
                {
                    return NotFound("No Books in the Library");
                }

                //Pagination Details
                var currentPage = model.PageNumber;
                var pageSize = model.PageSize;
                var items = books.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                var pagination = new
                {
                    TotalCount = books.Count(),
                    PageSize = pageSize,
                    CurrentPage = currentPage,
                    TotalPages = (int)Math.Ceiling(books.Count() / (double)pageSize),
                    PreviousPage = model.PageNumber > 1 ? "Yes" : "No",
                    NextPage = model.PageNumber < books.Count() ? "Yes" : "No"
                };
                HttpContext.Response.Headers.Add("Pagination-Header", JsonConvert.SerializeObject(pagination));
                if (items.Count == 0) return NotFound("No items to display");
                return Ok(items);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Fatal Error");
            }
        }
        /// <summary>
        /// Gets Paginated List according to Search Query
        /// </summary>
        [HttpGet("search")]
        public IActionResult SearchContacts([FromQuery] PagingParameters model)
        {
            try
            {
                var books = _bookRepo.GetBooks().ToList();
                if (books.Count <= 0)
                {
                    return NotFound("No Books in this library");
                }
                //Check for valid search query
                if (!string.IsNullOrEmpty(model.SearchQuery))
                {
                    books = books.Where(x => x.ISBN.ToLower().Contains(model.SearchQuery.ToLower())
                    || x.Title.ToLower().Contains(model.SearchQuery.ToLower())).ToList();

                }

                //Pagination Details
                var currentPage = model.PageNumber;
                var pageSize = model.PageSize;
                var items = books.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                var pagination = new
                {
                    TotalCount = books.Count(),
                    PageSize = pageSize,
                    CurrentPage = currentPage,
                    TotalPages = (int)Math.Ceiling(books.Count() / (double)pageSize),
                    PreviousPage = model.PageNumber > 1 ? "Yes" : "No",
                    NextPage = model.PageNumber < books.Count() ? "Yes" : "No"
                };
                HttpContext.Response.Headers.Add("SearchPagination-Header", JsonConvert.SerializeObject(pagination));
                if (items.Count == 0) return NotFound("No items to display");
                return Ok(items);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }
        }

        /// <summary>
        /// Add new book
        /// </summary>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post([FromBody] BookDetailsDTO bookToAdd)
        {
            
            //var count = _bookRepo.GetBooks().Count();
            //var book = _bookService.CreateBook(bookToAdd);
            var res = await _bookService.RegisterBook(bookToAdd);
            if (res == null)
            {
                return BadRequest("Book ISBN Exist");
            }
            var bookSuccessfullyAdded = await _bookService.Add(res);

            //Check to see if Contact was Added
            if (bookSuccessfullyAdded)
            {
                return Created($"api/Contact/{res.Id}", "Book Created Successfully");
            }

            return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");


        }

        /// <summary>
        /// Update Book Details
        /// </summary>
        [HttpPut("update/{isbn}")]
        public async Task<IActionResult> UpdateBook(string isbn, BookDetailsDTO bookToUpdate)
        {
            try
            {
                var book = _bookRepo.GetBook(isbn);
                if (book == null)
                {
                    return NotFound($"Could not find book with isbn of {isbn}");
                }

                book = _mapper.Map<Book>(bookToUpdate);
                await _bookRepo.Update(book);

                return Ok(_mapper.Map<BookDetailsDTO>(book));

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }

        }

        /// <summary>
        /// Delete book by ISBN
        /// </summary>
        [HttpDelete("delete/{isbn}")]
        public async Task<IActionResult> Delete(string isbn)
        {
            try
            {
                var book = _bookRepo.GetBook(isbn);
                if (book == null) return NotFound("Contact Not Found");
                await _bookRepo.Delete(book);
                return Ok($"The Book with isbn: {isbn} has been removed");

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }
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

        [HttpGet]
        [Route("all-books")]
        public IActionResult Get()
        {
            var books = _bookService.GetAllBooks();
            if (books == null) return NotFound("No book found");
            return Ok(books);
        }
    }
}