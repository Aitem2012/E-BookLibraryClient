using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.DTO
{
    public class PhotoUpdateDTO
    {
        public IFormFile PhotoUrl { get; set; }
    }
}
