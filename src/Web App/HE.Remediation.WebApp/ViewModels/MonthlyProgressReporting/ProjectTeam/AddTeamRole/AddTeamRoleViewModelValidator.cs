using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.AddTeamRole;

public class AddTeamRoleViewModelValidator : AbstractValidator<AddTeamRoleViewModel>
{
    public AddTeamRoleViewModelValidator()
    {
        RuleFor(x => x.TeamRole)
            .NotNull()
            .WithMessage("Please select a role you wish to add");
    }
}
