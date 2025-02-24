using HE.Remediation.Core.Data.StoredProcedureResults.SubContractorRatings;

namespace HE.Remediation.WebApp.ViewModels.Shared.SubContractorRatings;

public class SubContractorCheckYourAnswersViewModel
{
    public string Action { get; set; }
    public string ReturnAction { get; set; }
    public bool IsSubmitted { get; set; }
    public IReadOnlyCollection<GetSubcontractorRatingsSummaryResult> SubContractorRatings { get; set; }
}