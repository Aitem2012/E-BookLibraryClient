using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.DTO.BookResponse
{
    public class BookResponseDTO
    {
        public string Title { get; set; }
        public string GenreName { get; set; }
        public string Language { get; set; }
        public string PublisherName { get; set; }
        public string  ISBN { get; set; }
        public int Pages { get; set; }
        public int Rating { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
    }
}
