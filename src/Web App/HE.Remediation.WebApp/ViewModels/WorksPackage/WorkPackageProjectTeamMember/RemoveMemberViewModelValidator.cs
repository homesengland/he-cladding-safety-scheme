using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeamMember;

public class RemoveMemberViewModelValidator : AbstractValidator<RemoveMemberViewModel>
{
    public RemoveMemberViewModelValidator()
    {
        RuleFor(x => x.Confirm)
            .NotNull()
            .WithMessage("Select an option");
    }
}
