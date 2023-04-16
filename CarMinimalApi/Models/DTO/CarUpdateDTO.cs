namespace CarMinimalApi.Models.DTO
{
    public class CarUpdateDTO
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public int Behavior { get; set; }
        public string? Description { get; set; }
    }
}
