using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.Core.Services;
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
        private readonly IAuthorRepository _author;
        private readonly IGenreRepository _genre;
        private readonly ICloudinaryService _cloudinaryService;

        //Book Controller Constructor
        public BookController(IBookRepository bookRepository, IMapper mapper, IAuthorRepository author, IGenreRepository genre, ICloudinaryService cloudinaryService)
        {
            _bookRepo = bookRepository;
            _mapper = mapper;
            _author = author;
            _genre = genre;
            _cloudinaryService = cloudinaryService;
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
            var bookExist = _bookRepo.GetBook(bookToAdd.ISBN);
            if (bookExist != null) return BadRequest("Book Exist");
            var genre = _genre.GetGenre(bookToAdd.GenreId);
            if (genre == null) return NotFound("Genre Does not Exist");

            await _genre.Update(genre);

            var author = _author.GetAuthor(bookToAdd.Author.Id);
            if (author == null) return NotFound("Author Does Not Exist");
            await _author.Update(author);

            var count = _bookRepo.GetBooks().Count();
            //var book = _mapper.Map<Book>(bookToAdd);
            var book = new Book
            {
                Title = bookToAdd.Title,
                Id = bookToAdd.Id,
                Description = bookToAdd.Description,
                Language = bookToAdd.Language,
                Photo = bookToAdd.Photo,
                ISBN = bookToAdd.ISBN,
                Pages = bookToAdd.Pages,
                Rating = bookToAdd.Rating,
            };
            var res = await _bookRepo.Add(book);

            //Check to see if Contact was Added
            if (_bookRepo.GetBooks().Count() > count && res)
            {
                return Created($"api/Contact/{book.Id}", "Book Created Successfully");
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
        /// Update photo by ISBN
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="photoUpdate"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("updatephoto/{isbn}")]
        public async Task<IActionResult> UpdatePhoto(string isbn, [FromForm] PhotoUpdateDTO photoUpdate)
        {
            var bookToUpdate = _bookRepo.GetBook(isbn);
            if (bookToUpdate == null) return NotFound($"Could not find book with ISBN of {isbn}");
            bookToUpdate.Photo = await _cloudinaryService.AddPatchPhoto(photoUpdate);
            var photoIsUpdated = await _bookRepo.Update(bookToUpdate);
            if (!photoIsUpdated)
            {
                return StatusCode(500, "Something went wrong, try again");
            }
            return Ok($"Photo Path Successfully Updated");
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
    }
}