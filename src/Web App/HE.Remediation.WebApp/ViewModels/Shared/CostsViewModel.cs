using HE.Remediation.Core.Data.StoredProcedureResults.Costs;

namespace HE.Remediation.WebApp.ViewModels.Shared;

public class CostsViewModel
{
    public bool IsPaymentRequest { get; set; } 

    public decimal? TotalGrantFunding { get; set; }

    public DateTimeOffset? ProjectStartDate { get; set; }

    public DateTimeOffset? ProjectEndDate { get; set; }

    public decimal? ApprovedGrantFunding { get; set; }

    public decimal? ThirdPartyContribution { get; set; }

    public bool IsPtfsPaymentPaid { get; set; }

    public decimal? PtfsPayment { get; set; }

    public string PtfsPaymentText { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public IList<MonthlyCostViewModel> MonthlyCosts { get; set; }

    public decimal? MonthlyCostsTotal { get; set; }

    public decimal? TotalGrantPaidToDate { get; set; }

    public decimal? UnprofiledGrantFunding { get; set; }

    public MonthlyCostViewModel FirstMonthCost { get; set; }

    public MonthlyCostViewModel FirstMonthAdditionalCost { get; set; }

    public string FirstMonthAdditionalCostText { get; set; }

    public decimal? CurrentMonthTotal { get; set; }

    public MonthlyCostViewModel FinalMonthCost { get; set; }
    public decimal? AdditionalPtfsPayment { get; set; }
    public bool? IsAdditionalPtfsPaid { get; set; }
}
