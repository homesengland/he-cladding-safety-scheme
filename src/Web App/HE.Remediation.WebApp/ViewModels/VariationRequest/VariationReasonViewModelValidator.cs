using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class VariationReasonViewModelValidator : AbstractValidator<VariationReasonViewModel>
{
    public VariationReasonViewModelValidator()
    {
        RuleFor(x => x.VariationSelection)
            .Must((model, VariationSelection) => HasSelection(model.IsTimescaleVariation, model.IsScopeVariation, model.IsCostVariation, model.IsThirdPartyContributionVariation))
            .WithMessage("You must select at least one option");

        RuleFor(x => x.IsCostVariation)
            .Equal(true)
            .When(x => (x.LastMonthlyPaymentCompleted || x.ClosingReportStarted) && x.IsTimescaleVariation.GetValueOrDefault(false))
            .WithMessage("Additional costs needed across additional timeframe. Please also submit a costs variation");

        RuleFor(x => x.IsTimescaleVariation)
            .Equal(true)
            .When(x => (x.LastMonthlyPaymentCompleted || x.ClosingReportStarted) && x.IsCostVariation.GetValueOrDefault(false))
            .WithMessage("No time left to apply additional costs. Please also submit a time variation");
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
