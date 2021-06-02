using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.ViewModel
{
    public class BookResponseViewModel
    {
        public string Title { get; set; }
        public string GenreName { get; set; }
        public string Photo { get; set; }
        public string AuthorsFirstName { get; set; }
        public string AuthorsLastName { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
    }
}
