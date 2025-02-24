using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ExistingTeamMemberViewModelValidator : AbstractValidator<ExistingTeamMemberViewModel>
{
    public ExistingTeamMemberViewModelValidator()
    {
        RuleFor(x => x.SameAsPrevious)
            .NotNull()
            .WithMessage("Please select Yes or No");

        RuleFor(x => x.SelectedPreviousTeamMember)
            .NotNull()
            .WithMessage("Please choose a name")
            .When(x => x.SameAsPrevious.HasValue && x.SameAsPrevious.Value);
    }
}