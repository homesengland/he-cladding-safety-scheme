using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class SoughtQuotesViewModelValidator : AbstractValidator<SoughtQuotesViewModel>
{
    public SoughtQuotesViewModelValidator()
    {
        RuleFor(x => x.QuotesSought)
            .NotNull()
            .WithMessage("Please select Yes or No");
    }
}
