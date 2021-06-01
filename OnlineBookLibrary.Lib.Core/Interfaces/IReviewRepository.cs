using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Interfaces
{
    public interface IReviewRepository : ICRUDRepository<Review>
    {
        public Review GetReview(int id);
        public IQueryable<Review> GetReviews();
        public IQueryable<Review> GetReviewsByUser(int id);
    }
}
