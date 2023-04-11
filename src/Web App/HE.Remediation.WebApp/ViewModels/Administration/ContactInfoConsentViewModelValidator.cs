using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class ContactInfoConsentViewModelValidator : AbstractValidator<ContactInfoConsentViewModel>
{
    public ContactInfoConsentViewModelValidator()
    {
        RuleFor(x => x.UserConsent)
            .NotNull()
            .WithMessage("Select an option");
    }
}
