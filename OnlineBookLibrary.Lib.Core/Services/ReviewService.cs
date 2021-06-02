using Microsoft.AspNetCore.Identity;
using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.DTO.ReviewRequest;
using OnlineBookLibrary.Lib.DTO.ReviewResponse;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _review;

        public ReviewService(IReviewRepository reviewRepository)
        {
            
            _review = reviewRepository;
            
        }
        public Review CreateReview(ReviewRequestDTO model)
        {
            return new Review
            {
                AppUser = model.AppUser,
                Book = model.Book,
                ReviewHeader = model.ReviewHeader,
                Ratings = model.Rating,
                ReviewBody = model.ReviewBody
            };
        }

        public async Task<bool> Add(Review model)
        {
            if (!await _review.Add(model)) return false;
            return true;
        }

        public IEnumerable<ReviewResponseDTO> GetReviewByBookId(int id)
        {
            var reviews = _review.GetReviewsByBookId(id);
            var reviewList = new List<ReviewResponseDTO>();
            foreach (var review in reviews)
            {
                reviewList.Add(new ReviewResponseDTO
                {
                    Rating = review.Ratings,
                    ReviewHeader = review.ReviewHeader,
                    ReviewBody = review.ReviewBody
                });
            }

            return reviewList;
        }
    }
}
