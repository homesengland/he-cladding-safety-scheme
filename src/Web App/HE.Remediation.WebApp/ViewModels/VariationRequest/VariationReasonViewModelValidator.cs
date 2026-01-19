using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class VariationReasonViewModelValidator : AbstractValidator<VariationReasonViewModel>
{
    public VariationReasonViewModelValidator()
    {
        RuleFor(x => x.VariationSelection)
            .Must((model, VariationSelection) => HasSelection(model.IsTimescaleVariation, model.IsScopeVariation, model.IsCostVariation, model.IsThirdPartyContributionVariation))
            .WithMessage("You must select at least one option");

        RuleFor(x => x.IsTimescaleVariation)
            .Equal(true)
            .When(x => (InsufficientMonthlyPayments(x)) && x.IsCostVariation.GetValueOrDefault(false))
            .WithMessage("No time left to apply additional costs. Please also submit a time variation");
    }

    private static bool InsufficientMonthlyPayments(VariationReasonViewModel model)
    {
        return model.NoMonthlyPaymentsOutstanding || model.ClosingReportStarted;
    }

    private static bool HasSelection(bool? IsTimescaleVariation, bool? IsScopeVariation, bool? IsCostVariation, bool? IsThirdPartyContributionVariation)
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
