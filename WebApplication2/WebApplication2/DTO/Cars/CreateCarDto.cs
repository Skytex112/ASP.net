using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DTO.Cars
{
    public class CreateCarDto
    {
        [Required]
        public string Brand { get; set; } = string.Empty;
        [Required]
        public string Model { get; set; } = string.Empty;
        [Range(1, 10000000)]
        public decimal Price { get; set; }
        public int Year { get; set; }
    }
}