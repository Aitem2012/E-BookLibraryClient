using Microsoft.EntityFrameworkCore;
using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Lib.Infrastructure.Implementations
{
    public class GenreRepository : IGenreRepository
    {
        private readonly LibraryDbContext _ctx;

        public GenreRepository(LibraryDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> Add(Genre model)
        {
            _ctx.Genres.Add(model);
            if (await _ctx.SaveChangesAsync() >= 1) return true;
            return false;
        }

        public async Task<bool> Delete(Genre model)
        {
            var genreToDelete = _ctx.Genres.FirstOrDefault(x => x.Id == model.Id);
            _ctx.Genres.Remove(genreToDelete);
            if (await _ctx.SaveChangesAsync() >= 1) return true;
            return false;
        }

        public IQueryable<Genre> GetBooksByGenre(int id)
        {
            return _ctx.Genres.Include(x => x.Books).Where(x => x.Id == id).Select(x => x);

        }

        public Genre GetGenre(int id)
        {
            return _ctx.Genres.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Genre> GetGenres()
        {
            return _ctx.Genres.Select(x => x);
        }

        public async Task<bool> Update(Genre model)
        {
            var genreToUpdate = _ctx.Genres.FirstOrDefault(x => x.Id == model.Id);
            _ctx.Entry(genreToUpdate).CurrentValues.SetValues(model);
            if (await _ctx.SaveChangesAsync() >= 1) return true;
            return false;
        }
    }
}
