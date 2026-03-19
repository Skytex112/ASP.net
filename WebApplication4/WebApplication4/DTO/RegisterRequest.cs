using System.ComponentModel.DataAnnotations;

namespace WebApplication4.DTO
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Range(13, 120)]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]{3,20}$",
            ErrorMessage = "UserName must be 3-20 characters, only letters, digits, underscore")]
        public string UserName { get; set; }
    }
}
