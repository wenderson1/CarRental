using FluentValidation;
using RentalCompany.Application.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Application.Validators
{
    public class CreateOrderInputValidator : AbstractValidator<CreateOrderInput>
    {
        public CreateOrderInputValidator()
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
                .WithMessage("Expected Return Date must be after or equal to Start Date");

            RuleFor(x => x.IdCustomer)
                .NotEmpty()
                .NotNull()
                .WithMessage("IdCustomer can't be empty or null.");

            RuleFor(x => x.IdCar)
                .NotEmpty()
                .NotNull()
                .WithMessage("IdCar can't be empty or null.");
        }
        private bool BeValidDate(DateTime date)
        {
            return date >= DateTime.Today;
        }
    }

}
