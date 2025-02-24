namespace HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest;

public class GetPaymentRequestResult
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public decimal PaymentRequestCost { get; set; }
    public decimal ScheduledAmountCost { get; set; }
    public string ReasonForChange { get; set; }
    public decimal AdditionalProjectCost { get; set; }
    public DateTime? DateDue { get; set; }
    public bool IsSubmitted { get; set; }
    public DateTime? DateSubmitted { get; set; }
    public Guid? ScheduleOfWorksCostProfileId { get; set; }
    public bool? AwareProcess { get; set; }
    public bool? AwareNoPrecedentForFuture { get; set; }
    public bool? PredictionsAccurate { get; set; }
}
