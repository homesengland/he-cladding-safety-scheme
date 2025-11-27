using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.Leaseholders;

public class HaveYouContactedViewModelValidator : AbstractValidator<HaveYouContactedViewModel>
{
    public HaveYouContactedViewModelValidator()
    {
        RuleFor(x => x.HasContacted)
            .NotEmpty().WithMessage("Please select an option.");
    }
}