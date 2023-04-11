using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.Administration
{
    public class AdminSecondaryContactDetailsViewModelValidator : AbstractValidator<AdminSecondaryContactDetailsViewModel>
    {
        public AdminSecondaryContactDetailsViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Enter secondary contact name")
                .MaximumLength(150)
                .WithMessage("Name must be less than 150 characters");

            RuleFor(x => x.ContactNumber)
                .NotEmpty()
                .WithMessage("Enter contact number")
                .NotValidTelephoneGB()
                .WithMessage("Enter a Phone number, like 01632960001, 07700900982");

            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .WithMessage("Enter email address")
                .EmailAddress()
                .WithMessage(@"Enter an email address in the correct format, like name@example.com")
                .NotValidEmailAddress();
        }
    }
}