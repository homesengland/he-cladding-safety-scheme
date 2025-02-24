using HE.Remediation.WebApp.ViewModels.ClosingReport.Shared;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class ReviewPaymentRequestViewModel : ClosingReportBaseViewModel
{
    public bool ChangeToMonthlyCost { get; set; }

    public decimal? TotalGrantFunding { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public decimal? UnprofiledFunding { get; set; }


    public string PaymentRequestName { get; set; }

    public decimal? ScheduledAmount { get; set; }    

    public decimal? RequestedAmount { get; set; }
    
    public string AdditionalCostTitle { get; set; }

    public decimal? AdditionalCost { get; set; }

    public string ReasonForChange { get; set; }    
}
