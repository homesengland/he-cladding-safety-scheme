using AutoMapper.Execution;
using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class FreeholderCompanyDetailsViewModelValidator : AbstractValidator<FreeholderCompanyDetailsViewModel>
    {
        public FreeholderCompanyDetailsViewModelValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage("Please enter a Company name")
                .MaximumLength(150)
                .WithMessage("Company name cannot exceed 150 characters");

            RuleFor(x => x.CompanyRegistrationNumber)
                .NotEmpty()
                .WithMessage("Enter a Company registration number")
                .MaximumLength(8)
                .WithMessage("Company registration number must be 8 characters or less")
                .Matches("^[a-zA-Z0-9]{8}$")
                .WithMessage("Company registration number must only contain letters and numbers");

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
                .WithMessage(@"Enter an Email address in the correct format, like name@example.com")
                .NotValidEmailAddress();

            RuleFor(x => x.ContactNumber)
                .NotEmpty()
                .WithMessage("Please enter a Phone number")
                .NotValidTelephoneGB()
                .WithMessage("Enter a Phone number, like 01632960001, 07700900982");

        }
    }
}
