using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class AddressViewModelValidator : AbstractValidator<AddressViewModel>
    {
        public AddressViewModelValidator()
        {
            RuleFor(x => x.NameNumber)
                .NotEmpty()
                .WithMessage("Please enter a Building name")
                .MaximumLength(150)
                .WithMessage("Building name or number cannot exceed 150 characters");

            RuleFor(x => x.AddressLine1)
                .NotEmpty()
                .WithMessage("Please enter a 1st line of address")
                .MaximumLength(150)
                .WithMessage("1st line of address cannot exceed 150 characters");

            RuleFor(x => x.AddressLine2)
                .MaximumLength(150)
                .WithMessage("2nd line of address cannot exceed 150 characters");

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("Please enter a town or city")
                .MaximumLength(150)
                .WithMessage("Town or city cannot exceed 150 characters");

            RuleFor(x => x.County)
                .MaximumLength(150)
                .WithMessage("County cannot exceed 150 characters");

            RuleFor(x => x.Postcode)
                .NotEmpty()
                .WithMessage("Please enter a postcode")
                .NotValidPostcode()
                .WithMessage("Valid Postcodes only ");
        }
    }
}
