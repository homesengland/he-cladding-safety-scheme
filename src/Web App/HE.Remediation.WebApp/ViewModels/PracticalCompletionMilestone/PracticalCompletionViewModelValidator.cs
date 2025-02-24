using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.PracticalCompletionMilestone;

public class PracticalCompletionViewModelValidator : AbstractValidator<PracticalCompletionViewModel>
{
    public PracticalCompletionViewModelValidator()
    {
        RuleFor(x => x.PracticalCompletionDate)
            .NotNull()
            .WithMessage("Practical completion date must be a valid date")
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("Practical completion date must be on or before today");
    }
}