using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookLibraryClient.ViewModel
{
    public class BookViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Genre's Name")]
        public string GenreName { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        [Display(Name ="Publisher Name")]
        [Required]
        public string PublisherName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Publication Date")]
        public DateTime PublicationDate { get; set; } = DateTime.Now;
        [Required]
        public string ISBN { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Date Added to Library")]
        public DateTime DateAddedToLibrary { get; set; } = DateTime.Now;
        [Required]
        [Display(Name ="Author's First Name")]
        public string AuthorsFirstName { get; set; }
        [Required]
        [Display(Name = "Author's Last Name")]
        public string AuthorsLastName { get; set; }
        [Required]
        public int Pages { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
