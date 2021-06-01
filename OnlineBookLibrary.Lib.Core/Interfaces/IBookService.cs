using OnlineBookLibrary.Lib.DTO;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Interfaces
{
    public interface IBookService
    {
        public Book CreateBook(BookDetailsDTO model);
    }
}
