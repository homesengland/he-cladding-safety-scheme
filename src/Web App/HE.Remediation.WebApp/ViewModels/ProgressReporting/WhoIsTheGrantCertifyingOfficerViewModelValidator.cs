using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class WhoIsTheGrantCertifyingOfficerViewModelValidator : AbstractValidator<WhoIsTheGrantCertifyingOfficerViewModel>
{
    public WhoIsTheGrantCertifyingOfficerViewModelValidator()
    {
        RuleFor(x => x.ProjectTeamMemberId)
            .NotNull()
            .WithMessage("Select a team member");
    }
}