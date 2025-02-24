namespace HE.Remediation.Core.Data.StoredProcedureParameters.PaymentRequest;

public class PaymentRequestThirdPartyContributionsChangedParameters
{
    public Guid PaymentRequestId { get; set; }

    public bool? ThirdPartyContributionsChanged { get; set; }
}
