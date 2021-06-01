using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.DTO.AuthorRegister;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Services
{
    public class AuthorServices : IAuthorService
    {
        public Author CreateAuthor(AuthorRegisterDTO model)
        {
            return new Author
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Books = model.Book
            };
        }
    }
}
