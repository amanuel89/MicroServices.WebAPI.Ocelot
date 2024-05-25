using FluentValidation;
using RideBackend.Domain.Models;

namespace RideBackend.Domain.Validator
{
    public class BankValidator : AbstractValidator<Bank>
    {
        public BankValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name can't be empty")
                .NotEmpty().WithMessage("Name can't be empty");

            RuleFor(x => x.AccountName)
                .NotNull().WithMessage("Account Name can't be empty")
                .NotEmpty().WithMessage("Account Name can't be empty");

            RuleFor(x => x.AccountNumber)
                .NotNull().WithMessage("Account Number can't be empty")
                .NotEmpty().WithMessage("Account Number can't be empty");

        }
    }
}
