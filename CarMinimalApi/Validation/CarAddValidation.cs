using FluentValidation;
using CarMinimalApi.Models.DTO;
namespace CarMinimalApi.Validation
{
    public class CarAddValidation:AbstractValidator<CarAddDTO>
    {
        public CarAddValidation() 
        {
            RuleFor(model => model.Mark).NotEmpty();
            RuleFor(model => model.RegistrationNumber).NotEmpty();
            RuleFor(model => model.Behavior).NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}
