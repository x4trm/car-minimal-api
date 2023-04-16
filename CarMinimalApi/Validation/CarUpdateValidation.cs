using CarMinimalApi.Models.DTO;
using FluentValidation;

namespace CarMinimalApi.Validation
{
    public class CarUpdateValidation:AbstractValidator<CarUpdateDTO>
    {
        public CarUpdateValidation()
        { 
            RuleFor(model => model.Id).NotEmpty().GreaterThan(0);
            RuleFor(model => model.RegistrationNumber).NotEmpty();
            RuleFor(model => model.Behavior).NotEmpty().GreaterThan(0);
        }
    }
}
