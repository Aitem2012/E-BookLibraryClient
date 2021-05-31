using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookLibraryClient.Lib.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public Genre Genre { get; set; }

        [Required]
        public string Language { get; set; }
        public string Photo { get; set; }
        public Publisher Publisher { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateAddedToLibrary { get; set; }
        public IEnumerable<Author> Author { get; set; }

        public int Pages { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}