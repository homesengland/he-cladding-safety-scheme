using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class HaveYouAppliedForBuildingControlViewModelValidator : AbstractValidator<HaveYouAppliedForBuildingControlViewModel>
{
    public HaveYouAppliedForBuildingControlViewModelValidator()
    {
        RuleFor(x => x.HasAppliedForBuildingControl)
            .NotNull()
            .WithMessage("Please select an option");
    }
}