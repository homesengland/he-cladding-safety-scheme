using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class WorksToCladdingCladdingAreaViewModelValidator : AbstractValidator<WorksToCladdingCladdingAreaViewModel>
    {
        public WorksToCladdingCladdingAreaViewModelValidator()
        {
            RuleFor(x => x.RecommendedTotalAreaCladding)
                .NotNull()
                .NotEqual(0)
                .WithMessage("Enter total cladding area");
        }
    }
}
