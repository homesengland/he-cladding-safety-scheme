using HE.Remediation.Core.Data.StoredProcedureResults.SubContractorRatings;
using HE.Remediation.WebApp.ViewModels.ClosingReport.Shared;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class SubContractorsViewModel : ClosingReportBaseViewModel
{
    public IReadOnlyCollection<GetSubContractorRatingsOverviewResult> SubContractors { get; set; }
}