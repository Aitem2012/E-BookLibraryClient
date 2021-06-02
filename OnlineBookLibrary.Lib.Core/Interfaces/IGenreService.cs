using OnlineBookLibrary.Lib.DTO.GenreRequest;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Interfaces
{
    public interface IGenreService
    {
        public Genre CreateGenre(GenreRegisterDTO model);
        public Genre GetGenreByName(string genreName);
    }
}
