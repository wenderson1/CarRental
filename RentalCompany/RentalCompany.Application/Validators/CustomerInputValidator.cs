using FluentValidation;
using RentalCompany.Application.Models.Input;

namespace RentalCompany.Application.Validators
{
    public class CustomerInputValidator : AbstractValidator<CustomerInput>
    {
        public CustomerInputValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(200)
                .WithMessage("Name must have between 2 and 200 characters.");

            RuleFor(x => x.CNPJ)
                .NotEmpty()
                .NotNull()
                .Length(14)
                .WithMessage("CNPJ must have 14 characters.");

            RuleFor(x => x.BirthDate)
                .NotNull()
                .Must(BeValidDate)
                .Must(BeValidAge)
                .WithMessage("Your age must be greater than 18.");

            RuleFor(x => x.CnhNumber)
                .NotEmpty()
                .NotNull()
                .Length(9)
                .WithMessage("CnhNumber must have 9 characters.");

            RuleFor(x => x.CnhType)
                .NotEmpty()
                .NotNull()
                .Must(BeValidCnhType)
                .WithMessage("CnhType must be A or AB.");

        }
        private bool BeValidCnhType(string cnhType)
        {

            if (cnhType == "A" || cnhType == "AB")
                return true;

            return false;
        }

        private bool BeValidDate(DateTime date)
        {
            return date <= (DateTime.Today);
        }

        private bool BeValidAge(DateTime date)
        {
            return DateTime.Today.Year - date.Year >= 18;
        }
    }
}
