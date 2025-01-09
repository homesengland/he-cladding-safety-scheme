using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class RequiresSubcontractorsViewModelValidator : AbstractValidator<RequiresSubcontractorsViewModel>
{
    public RequiresSubcontractorsViewModelValidator()
    {
        RuleFor(x => x.RequiresSubcontractors)
            .NotNull()
            .WithMessage("Please select Yes or No");
    }
}