using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.DTO.PublisherRegister;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherRepository _publisher;

        public PublisherController(IPublisherRepository publisher)
        {
            _publisher = publisher;
        }

        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> Post([FromBody] PublisherRegisterDTO model)
        {
<<<<<<< HEAD
            var publisherExist = _publisher.GetPublisher(model.Id);
=======
            var publisherExist = _publisher.GetPublisher(model.PublisherName);
>>>>>>> e1d96428121e87aa11b38403acb0c4f4af5d6831
            if (publisherExist != null)
            {
                return BadRequest("Publisher Exist");
            }

            var genre = new Publisher
            {
                PublisherName = model.PublisherName
            };
            var genreIsAdded = await _publisher.Add(genre);
            if (!genreIsAdded)
            {
                return StatusCode(500, "Publisher could not be added");
            }
            return Ok("Publisher Successfully Registered");
        }

        [HttpGet]
        [Route("get-all-publisher")]
        public IActionResult Get()
        {
            var allGenre = _publisher.GetPublishers();
            if (allGenre == null) return BadRequest("No Publisher Available");

            return Ok(allGenre.ToList());
        }

        [HttpGet]
        [Route("{name}")]
<<<<<<< HEAD
        public IActionResult GetPublisher(int id)
        {
            try
            {
                var publisher = _publisher.GetPublisher(id);
                if (publisher == null) return BadRequest($"No Publisher with the name of {id}");
=======
        public IActionResult GetPublisher(string name)
        {
            try
            {
                var publisher = _publisher.GetPublisher(name);
                if (publisher == null) return BadRequest($"No Publisher with the name of {name}");
>>>>>>> e1d96428121e87aa11b38403acb0c4f4af5d6831

                return Ok(publisher);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }

        }


        [HttpPut]
        [Route("update/{name}")]
<<<<<<< HEAD
        public async Task<IActionResult> UpdatePublisher(int id, PublisherRegisterDTO model)
        {
            try
            {
                var publisher = _publisher.GetPublisher(id);
                if (publisher == null) return BadRequest($"No Publisher with the name of {id}");
=======
        public async Task<IActionResult> UpdatePublisher(string name, PublisherRegisterDTO model)
        {
            try
            {
                var publisher = _publisher.GetPublisher(name);
                if (publisher == null) return BadRequest($"No Publisher with the name of {name}");
>>>>>>> e1d96428121e87aa11b38403acb0c4f4af5d6831

                publisher.PublisherName = model.PublisherName;

                await _publisher.Update(publisher);
                return Ok("Publisher has been successfully updated");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }
        }
        [HttpDelete]
        [Route("delete/{name}")]
<<<<<<< HEAD
        public async Task<IActionResult> DeletePublisher(int id)
        {
            try
            {
                var publisher = _publisher.GetPublisher(id);
                if (publisher == null) return BadRequest($"No Publisher with the name of {id}");
=======
        public async Task<IActionResult> DeletePublisher(string name)
        {
            try
            {
                var publisher = _publisher.GetPublisher(name);
                if (publisher == null) return BadRequest($"No Publisher with the name of {name}");
>>>>>>> e1d96428121e87aa11b38403acb0c4f4af5d6831

                await _publisher.Delete(publisher);
                return Ok("Publisher has been successfully deleted");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }
        }
    }
}
