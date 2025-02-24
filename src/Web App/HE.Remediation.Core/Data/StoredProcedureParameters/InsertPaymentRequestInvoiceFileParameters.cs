namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class InsertPaymentRequestInvoiceFileParameters
{
    public Guid ApplicationId { get; set; }
    public Guid PaymentRequestId { get; set; }
    public Guid FileId { get; set; }
}