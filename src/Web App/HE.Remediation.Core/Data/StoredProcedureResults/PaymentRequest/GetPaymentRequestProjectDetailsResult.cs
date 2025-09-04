using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest;

public class GetPaymentRequestProjectDetailsResult
{
    public Guid Id { get; set; }
    public Guid PaymentRequestId { get; set; }

    public bool? UnsafeCladdingRemoved { get; set; }

    public DateTime? UnsafeCladdingRemovedDate { get; set; }

    public bool? CostsChanged { get; set; }

    public bool? ProjectDatesChanged { get; set; }

    public bool? UserEnteredCostsChanged { get; set; }

    public bool? ThirdPartyContributionsChanged { get; set; }

    public DateTime? LeaseholderResidentCommunicationDate { get; set; }
}
