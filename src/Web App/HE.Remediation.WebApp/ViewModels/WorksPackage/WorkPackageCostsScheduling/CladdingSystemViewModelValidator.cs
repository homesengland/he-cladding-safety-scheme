using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class CladdingSystemViewModelValidator : AbstractValidator<CladdingSystemViewModel>
{
    public CladdingSystemViewModelValidator()
    {
        RuleFor(x => x.IsBeingRemoved)
            .NotEmpty()
            .WithMessage("Select yes if this cladding is being removed");
    }
}
