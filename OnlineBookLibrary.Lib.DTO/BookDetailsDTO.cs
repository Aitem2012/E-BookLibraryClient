using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.DTO
{
    public class BookDetailsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }        
        public Genre Genre { get; set; }
        public string Language { get; set; }
        public string Photo { get; set; }
        public Publisher Publisher { get; set; }

        public DateTime PublicationDate { get; set; } = DateTime.Now;
        
        public string ISBN { get; set; }

        public DateTime DateAddedToLibrary { get; set; } = DateTime.Now;
        public Author Author { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
    }
}
