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

            RuleFor(x => x.CompanyRegistrationNumber)
             .NotEmpty()
             .WithMessage("Enter the Company Registration Number")
             .MinimumLength(4)
             .WithMessage("Company Registration Number must be between 4 and 8 digits")
             .MaximumLength(8)
             .WithMessage("Company Registration Number must be between 4 and 8 digits")
             .Matches("^[a-zA-Z0-9]{4,8}$")
             .WithMessage("Company Registration Number must contain only alphanumeric characters");

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
