using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities.RightToManage;

public class WhenRightToManageAcquiredViewModelValidator : AbstractValidator<WhenRightToManageAcquiredViewModel>
{
    public WhenRightToManageAcquiredViewModelValidator()
    {
        RuleFor(x => x.RightToManageAcquisitionDate)
            .NotNull()
            .WithMessage("Enter a date")
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("The date entered must be in the past");
    }
}