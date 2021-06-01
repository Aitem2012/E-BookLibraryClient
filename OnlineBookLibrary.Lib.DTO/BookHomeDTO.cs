using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.DTO
{
    public class BookHomeDTO
    {
        public string Title { get; set; }
      
        public Genre Genre { get; set; }
        public string Photo { get; set; }
        public IEnumerable<Author> Author { get; set; }
        
        public int Rating { get; set; }
    }
}
