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
                var authorExist = _author.GetAuthor(model.Id);
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
        public IActionResult GetAuthor(int id)
        {
            try
            {
                var author = _author.GetAuthor(id);
                if (author == null) return BadRequest($"No Author with the ID of {id}");

                return Ok(author);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }

        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateAuthor(int  id, AuthorRegisterDTO model)
        {
            try
            {
                var author = _author.GetAuthor(id);
                if (author == null) return BadRequest($"No Author with the id of {id}");

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
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = _author.GetAuthor(id);
                if (author == null) return BadRequest($"No Author with the name of {id}");

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
