using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class VariationReasonViewModelValidator : AbstractValidator<VariationReasonViewModel>
{
    public VariationReasonViewModelValidator()
    {
        RuleFor(x => x.VariationSelection)
            .Must((model, VariationSelection) => HasSelection(model.IsTimescaleVariation, model.IsScopeVariation, model.IsCostVariation, model.IsThirdPartyContributionVariation))
            .WithMessage("You must select at least one option");
    }

    private bool HasSelection(bool? IsTimescaleVariation, bool? IsScopeVariation, bool? IsCostVariation, bool? IsThirdPartyContributionVariation)
    {
        if
        (
            !IsTimescaleVariation.GetValueOrDefault(false) &
            !IsScopeVariation.GetValueOrDefault(false) &
            !IsCostVariation.GetValueOrDefault(false) &
            !IsThirdPartyContributionVariation.GetValueOrDefault(false)
        )
        {
            return false;
        }

        return true;
    }
}
