namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.Start;

public class CheckStatusResponse
{
    public bool IsSubmitted { get; set; }

    public bool IsExpired { get; set; }
}
