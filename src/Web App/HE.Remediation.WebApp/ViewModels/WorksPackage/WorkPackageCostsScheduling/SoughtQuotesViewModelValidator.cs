using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class SoughtQuotesViewModelValidator : AbstractValidator<SoughtQuotesViewModel>
{
    public SoughtQuotesViewModelValidator()
    {
        RuleFor(x => x.SoughtQuotes)
            .NotNull()
            .WithMessage("Please select Yes or No");
    }
}