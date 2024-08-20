using System.ComponentModel.DataAnnotations;

namespace BlogPlatform.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Username must be between 8 and 15 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Username must contain at least one uppercase letter, one lowercase letter, and one number.")]
        public string Username { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 15 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number.")]
        public string PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
