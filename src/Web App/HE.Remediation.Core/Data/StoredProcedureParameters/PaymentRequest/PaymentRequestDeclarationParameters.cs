namespace HE.Remediation.Core.Data.StoredProcedureParameters.PaymentRequest;

public class PaymentRequestDeclarationParameters
{
    public bool? AwareProcess { get; set; }
    public bool? AwareNoPrecedentForFuture { get; set; }
    public bool? PredictionsAccurate { get; set; }
}
