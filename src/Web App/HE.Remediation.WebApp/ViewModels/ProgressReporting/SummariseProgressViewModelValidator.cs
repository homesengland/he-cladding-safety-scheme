using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class SummariseProgressViewModelValidator : AbstractValidator<SummariseProgressViewModel>
    {
        public SummariseProgressViewModelValidator()
        {
            RuleFor(x => x.IsSupportNeeded)
                .NotNull()
                .WithMessage("Please select Yes or No");

            RuleFor(x => x.ProgressSummary)
                .NotEmpty()
                .WithMessage("Enter a summary of your risks and blockers this month")
                .MaximumLength(1000)
                .WithMessage("Summary of your risks and blockers cannot exceed 1000 characters");
        }
    }
}
