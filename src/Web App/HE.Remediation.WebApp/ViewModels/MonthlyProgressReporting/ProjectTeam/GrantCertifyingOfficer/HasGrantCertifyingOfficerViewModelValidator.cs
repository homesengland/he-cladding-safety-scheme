using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class HasGrantCertifyingOfficerViewModelValidator : AbstractValidator<HasGrantCertifyingOfficerViewModel>
{
    public HasGrantCertifyingOfficerViewModelValidator()
    {
        RuleFor(x => x.DoYouHaveAGrantCertifyingOfficer)
            .NotNull()
            .WithMessage("Select Yes or No");
    }
}