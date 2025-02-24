using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeam;

public class ProjectTeamViewModelValidator : AbstractValidator<ProjectTeamViewModel>
{
    public ProjectTeamViewModelValidator()
    {
        RuleFor(x => x.MissingRoles)
            .Must(x => x is null || x.Count == 0)
            .WithMessage("Add missing team members");

        RuleFor(x => x.HasChasCertificationValue)
            .Must(x => x != false)
            .WithMessage("State if your lead contractor has obtained CHAS Elite certification (Common Assessment Standard)");
    }
}