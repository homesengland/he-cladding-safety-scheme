using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest;

public class PaymentRequestResult
{
    public Guid? Id { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? DateDue { get; set; }

    public DateTime? DateSubmitted { get; set; }

    public EPaymentRequestTaskStatus TaskStatusId { get; set; }

    public bool IsFinalPayment { get; set; }

    public bool IsExpired { get; set; }

    public bool IsSubmitted { get; set; }
}
