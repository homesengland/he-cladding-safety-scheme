using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class FireRiskAssessorDetailsViewModelValidator: AbstractValidator<FireRiskAssessorDetailsViewModel>
    {
        public FireRiskAssessorDetailsViewModelValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage("Please enter a Company name")
                .MaximumLength(150)
                .WithMessage("Company name cannot exceed 150 characters");

            RuleFor(x => x.CompanyNumber)
                .NotEmpty()
                .WithMessage("Please enter a Company number")
                .Matches("^[a-zA-Z0-9]{8}$")
                .WithMessage("Please enter a valid Company registration number");

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

            RuleFor(x => x.Telephone)
                .NotEmpty()
                .WithMessage("Please enter a Phone number")
                .NotValidTelephoneGB()
                .WithMessage("Enter a Phone number, like 01632960001, 07700900982");
        }
    }
}
