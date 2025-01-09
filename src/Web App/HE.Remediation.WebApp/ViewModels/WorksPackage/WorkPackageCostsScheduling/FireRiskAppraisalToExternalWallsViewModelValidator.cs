using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class FireRiskAppraisalToExternalWallsViewModelValidator : AbstractValidator<FireRiskAppraisalToExternalWallsViewModel>
{
    public FireRiskAppraisalToExternalWallsViewModelValidator()
    {
        RuleFor(x => x.CladdingSystems)
            .Must(x => x.All(s => s.CladdingSystemTaskStatusId is ETaskStatus.Completed))
            .When(x => x.CladdingSystems is not null && x.CladdingSystems.Any())
            .WithMessage("Complete all cladding system sections");
    }
}
