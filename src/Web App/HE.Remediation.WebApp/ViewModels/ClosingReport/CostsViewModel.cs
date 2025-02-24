using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class CostsViewModel : ClosingReportInformationViewModel
{
    public decimal? TotalGrantFunding { get; set; }

    public DateTimeOffset? ProjectStartDate { get; set; }

    public DateTimeOffset? ProjectEndDate { get; set; }

    public decimal? ApprovedGrantFunding { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public int ProjectDuration { get; set; }

    public IList<MonthlyCostViewModel> PaidCosts { get; set; }

    public decimal? FinalMonthTotal { get; set; }

    public decimal? UnprofiledGrantFunding { get; set; }

    public MonthlyCostViewModel FinalMonthCost { get; set; }

    public bool SubcontractorsRequired { get; set; }
}

public class MonthlyCostViewModel
{
    public Guid? Id { get; set; }

    public string MonthName { get; set; }

    public string AmountText { get; set; }

    public decimal? Amount => decimal.TryParse(AmountText, out var amount) ? amount : null;

    public bool Paid { get; set; }

    public EPaymentRequestStatus Status { get; set; }
}
