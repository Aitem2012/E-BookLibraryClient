using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.DTO.GenreRequest;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genre;

        public GenreController(IGenreRepository genre)
        {
            _genre = genre;
        }

        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> Post([FromBody] GenreRegisterDTO model)
        {
            var genreExist = _genre.GetGenre(model.GenreName);
            if (genreExist != null)
            {
                return BadRequest("Genre Exist");
            }

            var genre = new Genre
            {
                GenreName = model.GenreName
            };
            var genreIsAdded = await _genre.Add(genre);
            if (!genreIsAdded)
            {
                return StatusCode(500, "Genre could not be added");
            }
            return Ok("Genre Successfully Registered");
        }

        [HttpGet]
        [Route("get-all-genre")]
        public IActionResult Get()
        {
            var allGenre = _genre.GetGenres();
            if (allGenre == null) return BadRequest("No Genres Available");

            return Ok(allGenre.ToList());
        }

        [HttpGet]
        [Route("{name}")]
        public IActionResult GetGenre(string name)
        {
            try
            {
                var genre = _genre.GetGenre(name);
                if (genre == null) return BadRequest($"No Genre with the name of {name}");

                return Ok(genre);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }

        }

        [HttpPut]
        [Route("update/{name}")]
        public async Task<IActionResult> UpdateGenre(string name, GenreRegisterDTO model)
        {
            try
            {
                var genre = _genre.GetGenre(name);
                if (genre == null) return BadRequest($"No Genre with the name of {name}");

                genre.GenreName = model.GenreName;

                await _genre.Update(genre);
                return Ok("Genre has been successfully updated");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteGenre(string name)
        {
            try
            {
                var genre = _genre.GetGenre(name);
                if (genre == null) return BadRequest($"No Genre with the name of {name}");

                await _genre.Delete(genre);
                return Ok("Genre has been successfully deleted");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }
        }
    }
}
