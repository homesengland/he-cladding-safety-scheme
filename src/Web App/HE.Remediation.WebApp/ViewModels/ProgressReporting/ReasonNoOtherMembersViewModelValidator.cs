using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ReasonNoOtherMembersViewModelValidator : AbstractValidator<ReasonNoOtherMembersViewModel>
{
    public ReasonNoOtherMembersViewModelValidator()
    {
        RuleFor(x => x.OtherMembersNotAppointedReason)
            .NotNull()
            .WithMessage("Please provide a reason")
            .MaximumLength(150)
            .WithMessage("Reason must be less than 150 characters");

        RuleFor(x => x.OtherMembersNeedsSupport)
            .NotNull()
            .WithMessage("Please select Yes or No");
    }
}
