using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.DTO.ReviewResponse
{
    public class ReviewResponseDTO
    {
        public string ReviewBody { get; set; }
        public string ReviewHeader { get; set; }
        public string ISBN { get; set; }
        public int Rating { get; set; }
    }
}
