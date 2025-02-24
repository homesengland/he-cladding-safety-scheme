using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class GrantFundingSignatoryDetailsViewModelValidator : AbstractValidator<GrantFundingSignatoryDetailsViewModel>
{
    public GrantFundingSignatoryDetailsViewModelValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Please enter a first name")
            .MaximumLength(150)
            .WithMessage("First name must be less than 150 characters");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Please enter a last name")
            .MaximumLength(150)
            .WithMessage("Last name must be less than 150 characters");

        RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .WithMessage("Enter email address")
                .EmailAddress()
                .WithMessage(@"Enter an email address in the correct format, like name@example.com")
                .NotValidEmailAddress();

        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage("Please enter a role")
            .MaximumLength(150)
            .WithMessage("Role must be less than 150 characters");
    }
}