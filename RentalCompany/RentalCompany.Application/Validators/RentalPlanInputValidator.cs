using FluentValidation;
using RentalCompany.Application.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Application.Validators
{
    public class RentalPlanInputValidator : AbstractValidator<RentalPlanInput>
    {
        public RentalPlanInputValidator()
        {
            RuleFor(x => x.StartDate)
                .NotNull()
                .NotEmpty()
                .Must(BeValidDate)
                .WithMessage("Start Date must be today or after.");

            RuleFor(x => x.ExpectedReturnDate)
                .NotEmpty()
                .NotNull()
                .Must(BeValidDate)
                .WithMessage("Expected Return Date must be today or after.")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("Expected Return Date must be after or equal to Start Datew");

        }
        private bool BeValidDate(DateTime date)
        {
            return date >= DateTime.Today;
        }
    }
}
