using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer;

public class ConfirmViewModelValidator : AbstractValidator<ConfirmViewModel>
{
    public ConfirmViewModelValidator()
    {
        RuleFor(e => e.CertifyingOfficerResponse)
            .NotNull()
            .WithMessage("Select yes, update or no.");
    }
}
