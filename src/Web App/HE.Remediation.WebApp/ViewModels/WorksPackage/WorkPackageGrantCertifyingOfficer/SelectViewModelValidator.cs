using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer;

public class SelectViewModelValidator : AbstractValidator<SelectViewModel>
{
    public SelectViewModelValidator()
    {
        RuleFor(e => e.SelectedProjectTeamMemberId)
            .NotNull()
            .WithMessage("Select your grant certifying officer");
    }
}
