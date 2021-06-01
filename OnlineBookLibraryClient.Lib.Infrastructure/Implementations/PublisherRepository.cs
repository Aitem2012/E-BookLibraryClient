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
    public class PublisherRepository : IPublisherRepository
    {
        private readonly LibraryDbContext _ctx;

        public PublisherRepository(LibraryDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> Add(Publisher model)
        {
            _ctx.Publishers.Add(model);
            if (await _ctx.SaveChangesAsync() >= 1) return true;
            return false;
        }

        public async Task<bool> Delete(Publisher model)
        {
            var publisherToDelete = _ctx.Publishers.FirstOrDefault(x => x.Id == model.Id);
            _ctx.Publishers.Remove(publisherToDelete);
            if (await _ctx.SaveChangesAsync() >= 1) return true;
            return false;
        }

        public Publisher GetPublisher(int id)
        {
            return _ctx.Publishers.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Publisher> GetPublishers()
        {
            return _ctx.Publishers.Select(x => x);
        }

        public IQueryable<Publisher> GetPublishersWithBooks(int id)
        {
            return _ctx.Publishers.Include(x => x.Book).Where(x => x.Id == id).Select(x => x);
        }

        public async Task<bool> Update(Publisher model)
        {
            var publisherToUpdate = _ctx.Publishers.FirstOrDefault(x => x.Id == model.Id);
            _ctx.Entry(publisherToUpdate).CurrentValues.SetValues(model);
            if (await _ctx.SaveChangesAsync() >= 1) return true;
            return false;
        }
    }
}
