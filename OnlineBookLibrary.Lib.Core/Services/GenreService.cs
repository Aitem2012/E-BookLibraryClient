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
        private readonly IGenreRepository _genre;

        public GenreService(IGenreRepository genre)
        {
            _genre = genre;
        }
        public Genre CreateGenre(GenreRegisterDTO model)
        {
            return new Genre
            {
                GenreName = model.GenreName,
                Books = model.Books
            };
        }

        public Genre GetGenreByName(string genreName)
        {
            var genres = _genre.GetGenres();
            return genres.FirstOrDefault(x => x.GenreName == genreName);
        }
    }
}
