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
        public Author GetAuthorByName(string firstName, string lastName);
        public IEnumerable<AuthorResponseDTO> GetAuthors();
        public AuthorResponseDTO GetAuthorById(int id);
        Task<bool> Update(int id, AuthorRegisterDTO model);
        Task<bool> Delete(int id);
        Task<bool> Add(AuthorRegisterDTO model);
    }
}
