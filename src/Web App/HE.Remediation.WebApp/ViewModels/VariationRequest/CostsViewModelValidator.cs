using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class CostsViewModelValidator : AbstractValidator<CostsViewModel>
    {
        public CostsViewModelValidator()
        {
            RuleFor(x => x.VariationCostsValidation)
                .Must(x => x == true)
                .WithMessage("No cost variations have been entered. At least one cost variation must be entered if Cost is selected as a variation reason.");
        }
    }
}
