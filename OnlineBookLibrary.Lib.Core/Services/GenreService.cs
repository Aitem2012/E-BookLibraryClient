using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.DTO.GenreRequest;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Services
{
    public class GenreService : IGenreService
    {
        public Genre CreateGenre(GenreRegisterDTO model)
        {
            return new Genre
            {
                GenreName = model.GenreName,
                Books = model.Books
            };
        }
    }
}
