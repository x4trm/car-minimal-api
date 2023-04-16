using System.ComponentModel.DataAnnotations;

namespace CarMinimalApi.Models.DTO
{
    public class CarAddDTO
    {
        [Required]
        public string Mark { get; set; }
        [Required]
        [MaxLength(12)]
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public int Behavior { get; set; }
        public DateTime YearOfProduction { get; set; }
        public string? Description { get; set; }
    }
}
