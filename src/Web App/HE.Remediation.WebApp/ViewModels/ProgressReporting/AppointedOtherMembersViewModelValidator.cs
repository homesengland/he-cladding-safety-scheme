using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class AppointedOtherMembersViewModelValidator : AbstractValidator<AppointedOtherMembersViewModel>
{
    public AppointedOtherMembersViewModelValidator()
    {
        RuleFor(x => x.OtherMembersAppointed)
            .NotNull()
            .WithMessage("Please select Yes or No");
    }
}
