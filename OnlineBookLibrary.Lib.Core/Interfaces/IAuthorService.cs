using OnlineBookLibrary.Lib.DTO.AuthorRegister;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Interfaces
{
    public interface IAuthorService
    {
        public Author CreateAuthor(AuthorRegisterDTO model);
    }
}
