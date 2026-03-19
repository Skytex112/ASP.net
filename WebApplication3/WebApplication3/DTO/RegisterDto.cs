using System.ComponentModel.DataAnnotations;

namespace WebApplication3.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Range(13, 120)]
        public int Age { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")]
        public string UserName { get; set; }
    }
}