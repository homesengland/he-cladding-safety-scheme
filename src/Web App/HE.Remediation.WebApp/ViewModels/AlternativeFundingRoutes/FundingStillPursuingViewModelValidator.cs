using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes
{
    public class FundingStillPursuingViewModelValidator : AbstractValidator<FundingStillPursuingViewModel>
    {
        public FundingStillPursuingViewModelValidator()
        {
            RuleFor(x => x.FundingStillPursuing)
                .NotEmpty()
                .WithMessage("Select an option");
        }
    }
}