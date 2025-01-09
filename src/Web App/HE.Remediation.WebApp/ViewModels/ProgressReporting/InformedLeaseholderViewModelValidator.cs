using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class InformedLeaseholderViewModelValidator : AbstractValidator<InformedLeaseholderViewModel>
{
    public InformedLeaseholderViewModelValidator()
    {
        RuleFor(x => x.LeaseholdersInformed)
            .NotNull()
            .WithMessage("Select an option");
    }
}
