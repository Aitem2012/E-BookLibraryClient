using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookLibraryClient.Lib.Model
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PublisherName { get; set; }
        public IEnumerable<Book> Book { get; set; }
    }
}