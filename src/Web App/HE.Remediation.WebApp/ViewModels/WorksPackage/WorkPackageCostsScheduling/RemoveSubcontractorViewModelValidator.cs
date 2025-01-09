using FluentValidation;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeamMember;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class RemoveSubcontractorViewModelValidator : AbstractValidator<RemoveSubcontractorViewModel>
{
    public RemoveSubcontractorViewModelValidator()
    {
        RuleFor(x => x.Confirm)
            .NotNull()
            .WithMessage("Select an option");
    }
}
