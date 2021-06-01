using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.DTO.AuthorRegister;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _author;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository author, IMapper mapper)
        {
            _author = author;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post([FromBody] AuthorRegisterDTO model)
        {
            try
            {
<<<<<<< HEAD
                var authorExist = _author.GetAuthor(model.Id);
=======
                var authorExist = _author.GetAuthor(model.FirstName);
>>>>>>> e1d96428121e87aa11b38403acb0c4f4af5d6831
                if (authorExist != null) return BadRequest("Author Already Exist");

                var author = new Author
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var authorIsSuccessfullyRegistered = await _author.Add(author);
                if (!authorIsSuccessfullyRegistered) return StatusCode(500, "Something went wrong. Try Again Later");
                return Ok("Author Successfully Registered");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }

        }

        [HttpGet]
        [Route("get-all-author")]
        public IActionResult Get()
        {
            try
            {
                var allAuthor = _author.GetAuthors();
                if (allAuthor == null) return BadRequest("No Authors Available");

                return Ok(allAuthor.ToList());
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }

        }

        [HttpGet]
        [Route("{id}")]
<<<<<<< HEAD
        public IActionResult GetAuthor(int id)
        {
            try
            {
                var author = _author.GetAuthor(id);
                if (author == null) return BadRequest($"No Author with the ID of {id}");
=======
        public IActionResult GetAuthor(string name)
        {
            try
            {
                var author = _author.GetAuthor(name);
                if (author == null) return BadRequest($"No Author with the name of {name}");
>>>>>>> e1d96428121e87aa11b38403acb0c4f4af5d6831

                return Ok(author);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }

        }

        [HttpPut]
        [Route("update/{id}")]
<<<<<<< HEAD
        public async Task<IActionResult> UpdateAuthor(int  id, AuthorRegisterDTO model)
        {
            try
            {
                var author = _author.GetAuthor(id);
                if (author == null) return BadRequest($"No Author with the id of {id}");
=======
        public async Task<IActionResult> UpdateAuthor(string  name, AuthorRegisterDTO model)
        {
            try
            {
                var author = _author.GetAuthor(name);
                if (author == null) return BadRequest($"No Author with the id of {name}");
>>>>>>> e1d96428121e87aa11b38403acb0c4f4af5d6831

                author.FirstName = model.FirstName;
                author.LastName = model.LastName;

                await _author.Update(author);
                return Ok("Author has been successfully updated");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }
        }
        [HttpDelete]
        [Route("delete/{id}")]
<<<<<<< HEAD
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = _author.GetAuthor(id);
                if (author == null) return BadRequest($"No Author with the name of {id}");
=======
        public async Task<IActionResult> DeleteAuthor(string name)
        {
            try
            {
                var author = _author.GetAuthor(name);
                if (author == null) return BadRequest($"No Author with the name of {name}");
>>>>>>> e1d96428121e87aa11b38403acb0c4f4af5d6831

                await _author.Delete(author);
                return Ok("Author has been successfully deleted");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }
        }
    }
}
