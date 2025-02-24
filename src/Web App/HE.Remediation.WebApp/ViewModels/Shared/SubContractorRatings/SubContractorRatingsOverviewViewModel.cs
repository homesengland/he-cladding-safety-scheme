using HE.Remediation.Core.Data.StoredProcedureResults.SubContractorRatings;

namespace HE.Remediation.WebApp.ViewModels.Shared.SubContractorRatings;

public class SubContractorRatingsOverviewViewModel
{
    public string Action { get; set; }
    public IReadOnlyCollection<GetSubContractorRatingsOverviewResult> SubContractors { get; set; }
}