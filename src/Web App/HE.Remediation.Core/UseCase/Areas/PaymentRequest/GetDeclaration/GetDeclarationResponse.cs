namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetDeclaration;

public class GetDeclarationResponse
{
    public bool? AwareProcess { get; set; }
	public bool? AwareNoPrecedentForFuture { get; set; }
	public bool? PredictionsAccurate { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }

    public bool IsExpired { get; set; }
}