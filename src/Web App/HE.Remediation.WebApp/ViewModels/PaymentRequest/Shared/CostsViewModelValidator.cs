using FluentValidation;
using HE.Remediation.Core.Helpers;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared
{
    public class CostsViewModelValidator: AbstractValidator<CostsViewModel>
    {
        public CostsViewModelValidator()
        {
            RuleFor(x => x.UnprofiledGrantFunding)
            .Cascade(CascadeMode.Stop) 
            .Must(UnprofiledGrantFundingGreaterThanZeroRounded)
            .WithMessage("You have over-allocated your funding. Please update your monthly cost profile to match your approved grant funding.")
            .Must(UnprofiledGrantFundingEqualToZeroRounded)
            .WithMessage("You have under-allocated your funding. Please update your monthly cost profile to match your approved grant funding.")
            .When(x => !x.IsItALastSchedulePayment, ApplyConditionTo.CurrentValidator);
        }

        private bool UnprofiledGrantFundingGreaterThanZeroRounded(CostsViewModel costs, decimal? _)
        {
            var calculatedCosts = CostsCalculationHelper.CalculateMonthlyCosts(new MonthlyCostsCalculationRequest
            {
                ApprovedGrantFunding = costs.ApprovedGrantFunding,
                GrantPaidToDate = costs.GrantPaidToDate,
                MonthlyCosts = costs.MonthlyCosts?.Select(x => x.Amount ?? 0) ?? new List<decimal>(),
                AdditionalCost = costs.AdditionalCost?.Amount ?? 0,
                CurrentCost = costs.CurrentMonth?.Amount ?? 0,
                FinalCost = costs.FinalMonthCost?.Amount ?? 0
            });

            return Math.Round(calculatedCosts.UnprofiledAmount) >= 0;
        }

        private bool UnprofiledGrantFundingEqualToZeroRounded(CostsViewModel costs, decimal? _)
        {
            var calculatedCosts = CostsCalculationHelper.CalculateMonthlyCosts(new MonthlyCostsCalculationRequest
            {
                ApprovedGrantFunding = costs.ApprovedGrantFunding,
                GrantPaidToDate = costs.GrantPaidToDate,
                MonthlyCosts = costs.MonthlyCosts?.Select(x => x.Amount ?? 0) ?? new List<decimal>(),
                AdditionalCost = costs.AdditionalCost?.Amount ?? 0,
                CurrentCost = costs.CurrentMonth?.Amount ?? 0,
                FinalCost = costs.FinalMonthCost?.Amount ?? 0
            });

            return Math.Round(calculatedCosts.UnprofiledAmount) == 0;
        }
    }
}
