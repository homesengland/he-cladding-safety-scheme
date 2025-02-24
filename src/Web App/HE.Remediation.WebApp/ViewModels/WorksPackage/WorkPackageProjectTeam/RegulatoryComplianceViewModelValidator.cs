using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeam;

public class RegulatoryComplianceViewModelValidator : AbstractValidator<RegulatoryComplianceViewModel>
{
    public RegulatoryComplianceViewModelValidator()
    {
        RuleFor(x => x.RegulatoryCompliancePersonId)
            .NotNull()
            .WithMessage("Select a team member");
    }
}