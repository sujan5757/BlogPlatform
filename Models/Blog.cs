using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public String UserID { get; set; }

        // public User? User { get; set; }

    }
}
