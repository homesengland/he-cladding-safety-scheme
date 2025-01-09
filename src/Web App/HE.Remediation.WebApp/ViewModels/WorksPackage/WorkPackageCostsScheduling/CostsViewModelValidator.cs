using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class CostsViewModelValidator : AbstractValidator<CostsViewModel>
{
    public CostsViewModelValidator()
    {
        RuleFor(x => x.PreliminariesComplete)
            .Equal(true)
            .WithMessage("Enter preliminaries, access costs,  main contractor's overheads and profit");

        RuleFor(x => x.OtherCostsComplete)
            .Equal(true)
            .WithMessage("Enter other costs");
    }
}