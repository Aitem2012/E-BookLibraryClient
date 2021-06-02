using Microsoft.AspNetCore.Mvc;
using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.DTO.ReviewRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _review;

        public ReviewController(IReviewService reviewService)
        {
            _review = reviewService;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Add(ReviewRequestDTO model)
        {
            var review = _review.CreateReview(model);

            if (!await _review.Add(review)) return StatusCode(500, "An Error Occur, try again!!!");
            return Ok("Review Added");
        }

        [HttpGet]
        [Route("get-book-review")]
        public IActionResult Get(int id)
        {
            return Ok(_review.GetReviewByBookId(id));
        }
    }
}
