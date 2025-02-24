namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetReviewPayment;

public class GetReviewPaymentResponse
{
    public bool? ChangeToMonthlyCost { get; set; }

    public decimal? TotalGrantFunding { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public decimal? UnprofiledFunding { get; set; }

    public string PaymentRequestName { get; set; }

    public decimal? ScheduledAmount { get; set; }

    public decimal? RequestedAmount { get; set; }

    public string AdditionalCostTitle { get; set; }

    public decimal? AdditionalCost { get; set; }

    public string ReasonForChange { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }

    public bool IsExpired { get; set; }
}