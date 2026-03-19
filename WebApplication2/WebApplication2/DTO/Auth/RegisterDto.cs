using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DTO.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Ім'я є обов'язковим")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Прізвище є обов'язковим")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email є обов'язковим")]
        [EmailAddress(ErrorMessage = "Невірний формат Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль є обов'язковим")]
        [MinLength(6, ErrorMessage = "Пароль має містити щонайменше 6 символів")]
        public string Password { get; set; } = string.Empty;
    }
}