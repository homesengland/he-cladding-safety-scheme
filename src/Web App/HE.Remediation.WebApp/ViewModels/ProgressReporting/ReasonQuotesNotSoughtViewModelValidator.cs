using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ReasonQuotesNotSoughtViewModelValidator : AbstractValidator<ReasonQuotesNotSoughtViewModel>
{
    public ReasonQuotesNotSoughtViewModelValidator()
    {
        RuleFor(x => x.WhyYouHaveNotSoughtQuotes)
            .NotNull()
            .WithMessage("Select why you have not sought quotes or issued a tender");

        RuleFor(x => x.QuotesNotSoughtReason)
            .NotNull()
            .WithMessage("Please tell us why")
            .MaximumLength(150)
            .WithMessage("Reason must be less than 150 characters");

        RuleFor(x => x.QuotesNeedsSupport)
            .NotNull()
            .WithMessage("Please select Yes or No");
    }
}
