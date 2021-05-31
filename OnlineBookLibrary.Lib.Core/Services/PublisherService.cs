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
        public Publisher CreatePublisher(PublisherRegisterDTO model)
        {
            return new Publisher
            {
                PublisherName = model.PublisherName,
                Book = model.Books
            };
        }
    }
}
