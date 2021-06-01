using Microsoft.EntityFrameworkCore;
using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.Lib.Infrastructure.Implementations
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly LibraryDbContext _ctx;

        public ReviewRepository(LibraryDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> Add(Review model)
        {
            _ctx.Reviews.Add(model);
            if (await _ctx.SaveChangesAsync() >= 1) return true;
            return false;
        }

        public async Task<bool> Delete(Review model)
        {
            var reviewToDelete = _ctx.Reviews.FirstOrDefault(x => x.Id == model.Id);
            _ctx.Reviews.Remove(reviewToDelete);
            if (await _ctx.SaveChangesAsync() >= 1) return true;
            return false;
        }

        public IQueryable<Review> GetReviewsByUser(int id)
        {
            return _ctx.Reviews.Include(x => x.AppUser).Where(x => x.Id == id).Select(x => x);
        }

        public Review GetReview(int id)
        {
            return _ctx.Reviews.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Review> GetReviews()
        {
            return _ctx.Reviews.Select(x => x);
        }

        public async Task<bool> Update(Review model)
        {
            var reviewToUpdate = _ctx.Reviews.FirstOrDefault(x => x.Id == model.Id);
            _ctx.Entry(reviewToUpdate).CurrentValues.SetValues(model);
            if (await _ctx.SaveChangesAsync() >= 1) return true;
            return false;
        }
    }
}
