using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeamMember;

public class AddViewModelValidator : AbstractValidator<AddViewModel> 
{
    public AddViewModelValidator()
    {
        RuleFor(x => x.TeamRole)
            .NotNull()
            .WithMessage("Please select a role you wish to add");
    }
}