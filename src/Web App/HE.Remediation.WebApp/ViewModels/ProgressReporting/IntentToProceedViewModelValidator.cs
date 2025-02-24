using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class IntentToProceedViewModelValidator : AbstractValidator<IntentToProceedViewModel>
{
    public IntentToProceedViewModelValidator()
    {
        RuleFor(x => x.IntentToProceedType)
            .NotNull()
            .WithMessage("Select an option")
            .IsInEnum()
            .WithMessage("Select an option");
    }
}