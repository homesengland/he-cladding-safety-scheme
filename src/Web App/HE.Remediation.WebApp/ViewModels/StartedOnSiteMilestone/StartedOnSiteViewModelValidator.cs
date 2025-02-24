using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.StartedOnSiteMilestone;

public class StartedOnSiteViewModelValidator : AbstractValidator<StartedOnSiteViewModel>
{
    public StartedOnSiteViewModelValidator()
    {
        RuleFor(x => x.StartedOnSiteDate)
            .NotNull()
            .WithMessage("Start on site date must be a valid date")
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("Start on site date must be on or before today");
    }
}