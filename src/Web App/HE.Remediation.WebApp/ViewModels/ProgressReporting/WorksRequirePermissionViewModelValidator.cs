using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class WorksRequirePermissionViewModelValidator : AbstractValidator<WorksRequirePermissionViewModel>
{
    public WorksRequirePermissionViewModelValidator()
    {
        RuleFor(x => x.PermissionRequired)
            .NotNull()
            .WithMessage("Please select Yes or No");
    }
}
