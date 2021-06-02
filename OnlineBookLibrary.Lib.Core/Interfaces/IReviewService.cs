using OnlineBookLibrary.Lib.DTO.ReviewRequest;
using OnlineBookLibrary.Lib.DTO.ReviewResponse;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Interfaces
{
    public interface IReviewService
    {
        public Review CreateReview(ReviewRequestDTO model);
        Task<bool> Add(Review model);
        IEnumerable<ReviewResponseDTO> GetReviewByBookId(int id);
    }
}
