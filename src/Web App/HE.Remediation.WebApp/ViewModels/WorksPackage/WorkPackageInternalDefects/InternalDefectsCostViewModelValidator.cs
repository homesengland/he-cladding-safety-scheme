using FluentValidation;
using HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;
namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageInternalDefects;

public class InternalDefectsCostViewModelValidator : AbstractValidator<InternalDefectsCostViewModel>
{
    public InternalDefectsCostViewModelValidator()
    {

        RuleFor(x => x.InternalDefectsCosts)
            .NotNull().WithMessage("Enter the cost of fixing any internal defects")
            .GreaterThanOrEqualTo(0).WithMessage("Cost cannot be negative");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Tell us how these costs are being funded")
            .When(x => x.InternalDefectsCosts.HasValue && x.InternalDefectsCosts.Value > 0);

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must be maximum 1000 characters");
    }
}
