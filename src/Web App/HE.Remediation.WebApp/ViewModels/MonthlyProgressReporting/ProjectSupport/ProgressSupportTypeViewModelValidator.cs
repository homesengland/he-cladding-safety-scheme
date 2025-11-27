using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectSupport;

public class ProgressSupportTypeViewModelValidator : AbstractValidator<ProgressSupportTypeViewModel>
{
    public ProgressSupportTypeViewModelValidator()
    {
        RuleFor(x => x.SupportTypes)
            .NotEmpty()
            .WithMessage("Please select at least one type of support needed.");

        RuleFor(x => x.SupportNeededReason)
            .NotEmpty()
            .WithMessage("Please provide details of support needed.")
            .MaximumLength(2000).WithMessage("Max number of characters exceeded.");
    }
}