using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProgrammePlan;

public class HasProjectPlanViewModelValidator : AbstractValidator<HasProjectPlanViewModel>
{
    public HasProjectPlanViewModelValidator()
    {
        RuleFor(x => x.HasProjectPlan)
            .NotNull()
            .WithMessage("Please select an option");
    }
}