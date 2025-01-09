namespace HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest;

public class GetPaymentRequestEndVersionDatesResult
{
    public int MostRecentVersion { get; set; }
    public DateTime? MostRecentEndDate { get; set; }
    public int PreviousVersionNo { get; set; }
    public DateTime? PreviousEndDate { get; set; }
}
