using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.Administration
{
    public class AdminContactDetailsViewModelValidator : AbstractValidator<AdminContactDetailsViewModel>
    {
        public AdminContactDetailsViewModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Please enter a First name")
                .MaximumLength(150)
                .WithMessage("First name must be less than 150 characters");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Please enter a Last name")
                .WithMessage("Last name must be less than 150 characters");

            RuleFor(x => x.ContactNumber)
                .NotEmpty()
                .WithMessage("Please enter a Contact number")
                .NotValidTelephoneGB()
                .WithMessage("Enter a Phone number, like 01632960001, 07700900982"); ;
        }
    }
}
