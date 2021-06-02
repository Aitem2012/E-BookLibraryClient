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
        private readonly IAuthorRepository _author;

        public AuthorServices(IAuthorRepository author)
        {
            _author = author;
        }
        public Author CreateAuthor(AuthorRegisterDTO model)
        {
            return new Author
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Books = model.Book
            };
        }

        public AuthorResponseDTO GetAuthorById(int id)
        {
            var author = _author.GetAuthors();
            var authorToReturn = author.FirstOrDefault(x => x.Id == id);
            if (authorToReturn == null) return null;
            return new AuthorResponseDTO
            {
                FirstName = authorToReturn.FirstName,
                LastName = authorToReturn.LastName
            };

        }

        public Author GetAuthorByName(string firstName, string lastName)
        {
            var authors = _author.GetAuthors();
            return authors.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
        }
        public IEnumerable<AuthorResponseDTO> GetAuthors()
        {
            var authors = _author.GetAuthors();
            if (authors.ToList().Count <= 0) return null;

            var authorsList = new List<AuthorResponseDTO>();
            foreach (var author in authors)
            {
                authorsList.Add(new AuthorResponseDTO
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName
                });
            }

            return authorsList;
        }
        public async Task<bool> Update(int id, AuthorRegisterDTO model)
        {
            var author = _author.GetAuthor(id);
            if (author == null) return false;

            author.LastName = model.LastName;
            author.FirstName = model.FirstName;

            if (!await _author.Update(author)) return false;
            return true;
        }
        

        public async Task<bool> Delete (int id)
        {
            var author = _author.GetAuthor(id);
            if (!await _author.Delete(author))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Add (AuthorRegisterDTO model)
        {
            var author = new Author
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            if (!await _author.Add(author))
            {
                return false;
            }
            return true;
        }
    }
}
