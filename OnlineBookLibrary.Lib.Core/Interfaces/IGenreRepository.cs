using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Interfaces
{
    public interface IGenreRepository : ICRUDRepository<Genre>
    {
        public Genre GetGenre(string genreName);
        public IQueryable<Genre> GetBooksByGenre(int id);
        public IQueryable<Genre> GetGenres();
    }
}
