using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.DTO.ReviewRequest
{
    public class ReviewRequestDTO
    {
        public string ReviewBody { get; set; }
      
        public string ReviewHeader { get; set; }
        public AppUser   AppUser { get; set; }
        public Book Book { get; set; }
        public int Rating { get; set; }
    }
}
