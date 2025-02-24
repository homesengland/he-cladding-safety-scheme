using HE.Remediation.Core.Data.StoredProcedureResults.SubContractorRatings;
using HE.Remediation.WebApp.ViewModels.ClosingReport.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest.SubcontractorSurvey;

public class SubContractorRatingsViewModel : ClosingReportBaseViewModel
{
    public GetSubContractorRatingResult Ratings { get; set; }
}