using HE.Remediation.Core.Data.StoredProcedureResults.SubContractorRatings;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractorCheckYourAnswers;

public class GetSubContractorCheckYourAnswersResponse
{
    public IReadOnlyCollection<GetSubcontractorRatingsSummaryResult> SubcontractorRatings { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
}