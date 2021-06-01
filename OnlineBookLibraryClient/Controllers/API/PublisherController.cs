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
            var publisherExist = _publisher.GetPublisher(model.PublisherName);
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
        public IActionResult GetPublisher(string name)
        {
            try
            {
                var publisher = _publisher.GetPublisher(name);
                if (publisher == null) return BadRequest($"No Publisher with the name of {name}");

                return Ok(publisher);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong!!");
            }

        }


        [HttpPut]
        [Route("update/{name}")]
        public async Task<IActionResult> UpdatePublisher(string name, PublisherRegisterDTO model)
        {
            try
            {
                var publisher = _publisher.GetPublisher(name);
                if (publisher == null) return BadRequest($"No Publisher with the name of {name}");

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
        public async Task<IActionResult> DeletePublisher(string name)
        {
            try
            {
                var publisher = _publisher.GetPublisher(name);
                if (publisher == null) return BadRequest($"No Publisher with the name of {name}");

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
