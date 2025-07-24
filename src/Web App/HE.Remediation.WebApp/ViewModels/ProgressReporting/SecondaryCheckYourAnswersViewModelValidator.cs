using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class SecondaryCheckYourAnswersViewModelValidator : AbstractValidator<SecondaryCheckYourAnswersViewModel>
{
    public SecondaryCheckYourAnswersViewModelValidator()
    {
        RuleFor(x => x.SupportNeededReason)
            .NotEmpty()
            .When(x => x.HelpNeeded == true)
            .WithMessage("Enter what you need support with");
    }
}