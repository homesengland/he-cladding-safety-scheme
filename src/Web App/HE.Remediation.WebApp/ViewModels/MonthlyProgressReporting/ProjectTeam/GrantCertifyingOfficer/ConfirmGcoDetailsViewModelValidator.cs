using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class ConfirmGcoDetailsViewModelValidator : AbstractValidator<ConfirmGcoDetailsViewModel>
{
    public ConfirmGcoDetailsViewModelValidator()
    {
        RuleFor(x => x.CertifyingOfficerResponse)
            .NotNull()
            .WithMessage("Select an option");
    }
}