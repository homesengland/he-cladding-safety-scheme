using FluentValidation;
using HE.Remediation.Core.Helpers;
using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class MilestonesViewModelValidator : AbstractValidator<MilestonesViewModel>
{
    public MilestonesViewModelValidator()
    {
        RuleForEach(x => x.Costs.MonthlyCosts).SetValidator(new MonthlyCostViewModelValidator());

        RuleFor(x => x.Costs.UnprofiledGrantFunding)
            .Cascade(CascadeMode.Stop) //Stop validating on first failure
            .Must(UnprofiledGrantFundingGreaterThanZeroRounded)
            .WithMessage("You have over-allocated your funding. Please update your monthly cost profile to match your approved grant funding.")
            .Must(UnprofiledGrantFundingEqualToZeroRounded)
            .WithMessage("You have under-allocated your funding. Please update your monthly cost profile to match your approved grant funding.");
    }

    private bool UnprofiledGrantFundingGreaterThanZeroRounded(MilestonesViewModel viewModel, decimal? _)
    {
        var costs = viewModel.Costs;
        var calculatedCosts = CostsCalculationHelper.CalculateMonthlyCosts(new MonthlyCostsCalculationRequest
        {
            ApprovedGrantFunding = costs.ApprovedGrantFunding,
            GrantPaidToDate = costs.GrantPaidToDate,
            MonthlyCosts = costs.MonthlyCosts.Select(x => x.Amount ?? 0)
        });

        return Math.Truncate(calculatedCosts.UnprofiledAmount) >= 0;
    }

    private bool UnprofiledGrantFundingEqualToZeroRounded(MilestonesViewModel viewModel, decimal? _)
    {
        var costs = viewModel.Costs;
        var calculatedCosts = CostsCalculationHelper.CalculateMonthlyCosts(new MonthlyCostsCalculationRequest
        {
            ApprovedGrantFunding = costs.ApprovedGrantFunding,
            GrantPaidToDate = costs.GrantPaidToDate,
            MonthlyCosts = costs.MonthlyCosts.Select(x => x.Amount ?? 0)
        });

        return Math.Truncate(calculatedCosts.UnprofiledAmount) == 0;
    }
}
