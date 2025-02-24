using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.BankAccount;

public class VerificationContactViewModelValidator : AbstractValidator<VerificationContactViewModel>
{
    public VerificationContactViewModelValidator()
    {
        RuleFor(x => x.ContactName)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(150)
            .WithMessage("Name cannot be more than 150 characters");

        RuleFor(x => x.ContactNumber)
            .NotEmpty()
            .WithMessage("Phone number is required")
            .MaximumLength(150)
            .WithMessage("Phone number cannot be more than 150 characters")
            .NotValidTelephoneGB();
    }
}