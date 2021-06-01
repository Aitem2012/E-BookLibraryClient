using System.ComponentModel.DataAnnotations;

namespace OnlineBookLibraryClient.Lib.Model
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ReviewBody { get; set; }
        [Required]
        public string ReviewHeader { get; set; }
        public AppUser AppUser { get; set; }
        public Book Book { get; set; }

    }
}