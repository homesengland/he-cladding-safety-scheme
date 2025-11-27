using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.TeamMember;

public class RemoveTeamMemberViewModelValidator : AbstractValidator<RemoveTeamMemberViewModel>
{
    public RemoveTeamMemberViewModelValidator()
    {
        RuleFor(x => x.Confirm)
            .NotNull()
            .WithMessage("Select an option");
    }
}
