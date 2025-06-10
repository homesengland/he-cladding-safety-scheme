using FluentValidation;

using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.Leaseholder
{
    public class LeaseHolderCommunicationPartyDetailsViewModelValidator : AbstractValidator<LeaseHolderCommunicationPartyDetailsViewModel>
    {
        public LeaseHolderCommunicationPartyDetailsViewModelValidator()
        {
            RuleFor(x => x.CompanyName)
                 .NotEmpty()
                 .WithMessage("Enter the company name")
                 .MaximumLength(150)
                 .WithMessage("Company name must not be more than 150 characters");

            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .WithMessage("Enter an email address")
                .NotValidEmailAddress()
                .WithMessage("Enter a valid email address")
                .MaximumLength(150)
                .WithMessage("Email address must not be more than 150 characters");

            RuleFor(x => x.ContactNumber)
                .NotEmpty()
                .WithMessage("Enter a contact number")
                .MaximumLength(128)
                .WithMessage("Contact number must not be more than 128 characters")
                .NotValidTelephoneGB();

            RuleFor(x => x.ContactName)
                .MaximumLength(150)
                .WithMessage("Contact name must not be more than 150 characters");
        }
    }
}
