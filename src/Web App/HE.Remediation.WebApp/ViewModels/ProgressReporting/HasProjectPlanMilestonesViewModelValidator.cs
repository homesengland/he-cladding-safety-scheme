using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class HasProjectPlanMilestonesViewModelValidator : AbstractValidator<HasProjectPlanMilestonesViewModel>
{
    public HasProjectPlanMilestonesViewModelValidator()
    {
        RuleFor(x => x.HasProjectPlanMilestones)
            .NotNull()
            .WithMessage("Select Yes or No");
    }
}