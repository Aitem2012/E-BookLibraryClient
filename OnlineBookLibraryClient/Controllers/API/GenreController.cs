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
<<<<<<< HEAD
            var genreExist = _genre.GetGenre(model.Id);
=======
            var genreExist = _genre.GetGenre(model.GenreName);
>>>>>>> e1d96428121e87aa11b38403acb0c4f4af5d6831
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
<<<<<<< HEAD
        [Route("{id}")]
        public IActionResult GetGenre(int id)
        {
            try
            {
                var genre = _genre.GetGenre(id);
                if (genre == null) return BadRequest($"No Genre with the name of {id}");
=======
        [Route("{name}")]
        public IActionResult GetGenre(string name)
        {
            try
            {
                var genre = _genre.GetGenre(name);
                if (genre == null) return BadRequest($"No Genre with the name of {name}");
>>>>>>> e1d96428121e87aa11b38403acb0c4f4af5d6831

                return Ok(genre);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }

        }

        [HttpPut]
<<<<<<< HEAD
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateGenre(int id, GenreRegisterDTO model)
        {
            try
            {
                var genre = _genre.GetGenre(id);
                if (genre == null) return BadRequest($"No Genre with the name of {id}");
=======
        [Route("update/{name}")]
        public async Task<IActionResult> UpdateGenre(string name, GenreRegisterDTO model)
        {
            try
            {
                var genre = _genre.GetGenre(name);
                if (genre == null) return BadRequest($"No Genre with the name of {name}");
>>>>>>> e1d96428121e87aa11b38403acb0c4f4af5d6831

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
<<<<<<< HEAD
        public async Task<IActionResult> DeleteGenre(int id)
        {
            try
            {
                var genre = _genre.GetGenre(id);
                if (genre == null) return BadRequest($"No Genre with the name of {id}");
=======
        public async Task<IActionResult> DeleteGenre(string name)
        {
            try
            {
                var genre = _genre.GetGenre(name);
                if (genre == null) return BadRequest($"No Genre with the name of {name}");
>>>>>>> e1d96428121e87aa11b38403acb0c4f4af5d6831

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
