using FluentValidation;
using User.Application.Models.Input;

namespace User.Application.Validators
{
    public class CarInputValidator : AbstractValidator<CarInput>
    {
        public CarInputValidator()
        {
            RuleFor(x => x.Year)
                .NotNull()
                .GreaterThan(2000)
                .WithMessage("Year cannot be null and must be greater than 2000.");

            RuleFor(x => x.Model)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(200)
                .WithMessage("Model cannot be null or empty, and must have to be between 2 and 200 characters,");

            RuleFor(x => x.LicensePlate)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(10)
                .WithMessage("LicensePlate cannot be null or empty, and must have to be between 2 and 1 characters.");

            RuleFor(x => x.Year)
                .Must(BeValidYear)
                .WithMessage("Year must be lower or equal the next year.");
        }

        private bool BeValidYear(int year)
        {
            return year <= Convert.ToInt16(DateTime.Today.Year) + 1;
        }
    }
}
