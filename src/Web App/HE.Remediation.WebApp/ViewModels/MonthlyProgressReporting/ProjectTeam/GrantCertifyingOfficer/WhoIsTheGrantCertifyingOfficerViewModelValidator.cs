using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class WhoIsTheGrantCertifyingOfficerViewModelValidator : AbstractValidator<WhoIsTheGrantCertifyingOfficerViewModel>
{
    public WhoIsTheGrantCertifyingOfficerViewModelValidator()
    {
        RuleFor(x => x.ProjectTeamMemberId)
            .NotNull()
            .WithMessage("Select a team member");
    }
}