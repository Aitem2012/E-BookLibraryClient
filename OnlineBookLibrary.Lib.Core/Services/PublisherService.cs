using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibrary.Lib.DTO.PublisherRegister;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisher;

        public PublisherService(IPublisherRepository publisher)
        {
            _publisher = publisher;
        }
        public Publisher CreatePublisher(PublisherRegisterDTO model)
        {
            return new Publisher
            {
                PublisherName = model.PublisherName,
                
            };
        }

        public Publisher GetPublisherByName(string publisherName)
        {
            var publishers = _publisher.GetPublishers();

            return publishers.FirstOrDefault(x => x.PublisherName == publisherName);
        }

        public async Task<bool> Add (Publisher model)
        {
           return await _publisher.Add(model);
        }
    }
}
