using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackagePlanningPermission;

public class WorksRequirePermissionViewModelValidator : AbstractValidator<WorksRequirePermissionViewModel>
{
    public WorksRequirePermissionViewModelValidator()
    {
        RuleFor(x => x.PermissionRequired)
            .NotNull()
            .WithMessage("Please select Yes or No");

        RuleFor(x => x.ReasonPermissionNotRequired)
            .NotNull()
            .When(x => x.PermissionRequired == false)
            .WithMessage("Please provide a reason");

        RuleFor(x => x.ReasonPermissionNotRequired)
            .MaximumLength(500)
            .WithMessage("Reason must be less than 500 characters");
    }
}
