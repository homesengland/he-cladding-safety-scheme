using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityPrimaryContactDetailsViewModelValidator : AbstractValidator<ResponsibleEntityPrimaryContactDetailsViewModel>
    {
        public ResponsibleEntityPrimaryContactDetailsViewModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Please enter a First name")
                .MaximumLength(150)
                .WithMessage("First name cannot exceed 150 characters");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Please enter a Last name")
                .MaximumLength(150)
                .WithMessage("Last name cannot exceed 150 characters");

            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .WithMessage("Please enter an Email address")
                .EmailAddress()
                .WithMessage(@"Enter an email address in the correct format, like name@example.com");

            RuleFor(x => x.ContactNumber)
                .NotEmpty()
                .WithMessage("Please enter a Phone number")
                .NotValidTelephoneGB()
                .WithMessage("Enter a Phone number, like 01632960001, 07700900982");
        }
    }
}
