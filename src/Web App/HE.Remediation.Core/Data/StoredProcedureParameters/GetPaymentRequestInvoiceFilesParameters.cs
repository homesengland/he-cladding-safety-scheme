namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class GetPaymentRequestInvoiceFilesParameters
{
    public Guid ApplicationId { get; set; }
    public Guid PaymentRequestId { get; set; }
}