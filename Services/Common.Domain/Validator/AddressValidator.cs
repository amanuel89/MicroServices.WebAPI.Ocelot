namespace RideBackend.Domain.Validator;
public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {

        RuleFor(x => x.AddressName)
              .NotNull().WithMessage("Address Name can't be null")
              .NotEmpty().WithMessage("Address Name can't be empty");
   
    }

}

