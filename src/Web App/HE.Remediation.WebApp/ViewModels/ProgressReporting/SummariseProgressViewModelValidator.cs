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
                .WithMessage("Enter a summary of this month's progress")
                .MaximumLength(500)
                .WithMessage("Summary of this month's progress cannot exceed 500 characters");

            RuleFor(x => x.GoalSummary)
                .NotEmpty()
                .WithMessage("Enter a summary of next month's goals")
                .MaximumLength(500)
                .WithMessage("Summary of next month's progress cannot exceed 500 characters");
        }
    }
}
