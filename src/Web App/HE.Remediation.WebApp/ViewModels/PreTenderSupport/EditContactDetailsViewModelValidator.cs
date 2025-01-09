using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.PreTenderSupport;

public class EditContactDetailsViewModelValidator : AbstractValidator<EditContactDetailsViewModel>
{
    public EditContactDetailsViewModelValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("Please enter an Email address")
            .EmailAddress()
            .WithMessage(@"Enter an email address in the correct format, like name@example.com")
            .NotValidEmailAddress();
    }
}
