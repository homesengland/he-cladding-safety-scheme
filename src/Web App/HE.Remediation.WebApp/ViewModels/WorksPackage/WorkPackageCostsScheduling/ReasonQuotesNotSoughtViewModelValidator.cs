using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class NoQuotesViewModelValidator : AbstractValidator<NoQuotesViewModel>
{
    public NoQuotesViewModelValidator()
    {
        RuleFor(x => x.Reason)
            .NotNull()
            .WithMessage("Please provide a reason")
            .MaximumLength(500)
            .WithMessage("Reason must be less than 500 characters");
    }
}
