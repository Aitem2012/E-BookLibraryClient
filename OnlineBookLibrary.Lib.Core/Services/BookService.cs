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
                Genre = model.Genre,
                Language = model.Language,
                Photo = model.Photo,
                Publisher = model.Publisher,
                PublicationDate = model.PublicationDate,
                ISBN = model.ISBN,
                DateAddedToLibrary = model.DateAddedToLibrary,
                Author = model.Author,
                Pages = model.Pages,
                Description = model.Description,
                Rating = model.Rating
            };
        }
    }
}
