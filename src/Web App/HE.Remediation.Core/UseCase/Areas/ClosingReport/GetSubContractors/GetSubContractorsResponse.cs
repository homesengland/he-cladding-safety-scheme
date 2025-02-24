using HE.Remediation.Core.Data.StoredProcedureResults.SubContractorRatings;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractors;

public class GetSubContractorsResponse
{
    public IReadOnlyCollection<GetSubContractorRatingsOverviewResult> SubContractors { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
}