using FluentValidation;

using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeam;

public class ProjectTeamViewModelValidator : AbstractValidator<ProjectTeamViewModel>
{
    public ProjectTeamViewModelValidator()
    {
        RuleFor(x => x.MissingRoles)
            .Must(x => x is null || !x.Any(role => role != ETeamRole.ManagingAgent))
            .WithMessage("Add missing team members");

        RuleFor(x => x.HasChasCertificationValue)
            .Must(x => x != false)
            .WithMessage("State if your lead contractor has obtained CHAS Elite certification (Common Assessment Standard)");
    }
}