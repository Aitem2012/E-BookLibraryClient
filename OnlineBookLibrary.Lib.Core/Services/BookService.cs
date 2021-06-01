using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.DTO;
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
    }
}
