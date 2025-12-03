using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.NoTeam;
public class ProjectTeamNoTeamViewModelValidator : AbstractValidator<ProjectTeamNoTeamViewModel>
{
    public ProjectTeamNoTeamViewModelValidator()
    {
        RuleFor(x => x.ReasonNoTeam)
.NotEmpty().WithMessage("Please provide a reason for not appointing any team members")
            .MaximumLength(150).WithMessage("Reason must be 150 characters or fewer");
    }
}
