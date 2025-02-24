namespace HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest;

public class GetPaymentRequestDeclarationResult
{
    public bool? AwareProcess { get; set; }
    public bool? AwareNoPrecedentForFuture { get; set; }
    public bool? PredictionsAccurate { get; set; }
}
