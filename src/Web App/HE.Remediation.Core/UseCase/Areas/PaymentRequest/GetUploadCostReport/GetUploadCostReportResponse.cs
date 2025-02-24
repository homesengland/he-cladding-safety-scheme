using HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetUploadCostReport;

public class GetUploadCostReportResponse
{
    public List<PaymentCostReportResult> AddedFiles { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }

    public bool IsExpired { get; set; }
}
