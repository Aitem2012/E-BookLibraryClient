using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Interfaces
{
    public interface IAuthorRepository : ICRUDRepository<Author>
    {
        public Author GetAuthor(int id);
        public IQueryable<Author> GetAuthors();
    }
}
