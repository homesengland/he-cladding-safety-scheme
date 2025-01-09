using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ConfirmGcoDetailsViewModelValidator : AbstractValidator<ConfirmGcoDetailsViewModel>
{
    public ConfirmGcoDetailsViewModelValidator()
    {
        RuleFor(x => x.CertifyingOfficerResponse)
            .NotNull()
            .WithMessage("Select an option");
    }
}