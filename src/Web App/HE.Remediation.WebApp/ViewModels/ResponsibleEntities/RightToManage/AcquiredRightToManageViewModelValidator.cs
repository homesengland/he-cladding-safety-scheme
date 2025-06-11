using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities.RightToManage;

public class AcquiredRightToManageViewModelValidator : AbstractValidator<AcquiredRightToManageViewModel>
{
    public AcquiredRightToManageViewModelValidator()
    {
        RuleFor(x => x.HasAcquiredRightToManage)
            .NotNull()
            .WithMessage("Please select Yes or No");
    }
}