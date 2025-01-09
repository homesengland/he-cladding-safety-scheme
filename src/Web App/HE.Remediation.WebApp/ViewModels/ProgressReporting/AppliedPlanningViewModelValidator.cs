using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class AppliedPlanningViewModelValidator : AbstractValidator<AppliedPlanningViewModel>
{
    public AppliedPlanningViewModelValidator()
    {
        RuleFor(x => x.AppliedForPlanningPermission)
            .NotNull()
            .WithMessage("Please select Yes or No");
    }
}
