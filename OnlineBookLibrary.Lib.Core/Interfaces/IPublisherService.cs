using OnlineBookLibrary.Lib.DTO.PublisherRegister;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Interfaces
{
    public interface IPublisherService
    {
        public Publisher CreatePublisher(PublisherRegisterDTO model);
        public Publisher GetPublisherByName(string publisherName);
        Task<bool> Add(Publisher model);
    }
}
