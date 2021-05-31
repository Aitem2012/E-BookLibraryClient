using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Lib.Infrastructure.Implementations
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _ctx;

        public AuthorRepository(LibraryDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> Add(Author model)
        {
            _ctx.Authors.Add(model);
            if (await _ctx.SaveChangesAsync() >= 1) return true;
            return false;
        }

        public async Task<bool> Delete(Author model)
        {
            var authorToDelete = _ctx.Authors.FirstOrDefault(x => x.Id == model.Id);
            _ctx.Authors.Remove(authorToDelete);
            if (await _ctx.SaveChangesAsync() >= 1) return true;
            return false;
        }

        public Author GetAuthor(string firstName)
        {
            return _ctx.Authors.FirstOrDefault(x => x.FirstName == firstName);
        }

        public IQueryable<Author> GetAuthors()
        {
            return _ctx.Authors.Select(x => x);
        }

        public async Task<bool> Update(Author model)
        {
            var authorToUpdate = _ctx.Authors.FirstOrDefault(x => x.Id == model.Id);
            _ctx.Entry(authorToUpdate).CurrentValues.SetValues(model);
            if (await _ctx.SaveChangesAsync() >= 1) return true;
            return false;
        }
    }
}
