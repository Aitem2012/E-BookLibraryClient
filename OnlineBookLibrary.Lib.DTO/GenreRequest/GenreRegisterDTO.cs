using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.DTO.GenreRequest
{
    public class GenreRegisterDTO
    {
        public string GenreName { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
