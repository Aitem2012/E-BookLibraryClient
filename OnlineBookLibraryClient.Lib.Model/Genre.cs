using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookLibraryClient.Lib.Model
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string GenreName { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}