using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Interfaces
{
    public interface IPublisherRepository : ICRUDRepository<Publisher>
    {
        public Publisher GetPublisher(string publisherName);
        public IQueryable<Publisher> GetPublishers();
        public IQueryable<Publisher> GetPublishersWithBooks(int id);
    }
}
