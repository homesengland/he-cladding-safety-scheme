using HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetLeaseholderResidentUploadEvidence;

public class GetLeaseholderResidentUploadEvidenceResponse
{
    public List<PaymentLeaseholderResidentUploadEvidenceResult> AddedFiles { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public int? LastCommunicationDateMonth { get; set; }
    public int? LastCommunicationDateYear { get; set; }

    public bool IsSubmitted { get; set; }

    public bool IsExpired { get; set; }
}
