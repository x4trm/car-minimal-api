namespace CarMinimalApi.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Mark { get; set; }
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public int Behavior { get; set; }
        public DateTime YearOfProduction { get; set; }
        public string? Description { get; set; }
    }
}
