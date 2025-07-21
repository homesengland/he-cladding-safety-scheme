using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressSupport.GetProgressSupport;

public class GetProgressSupportResponse
{
    public IList<EProgressReportSupportType> SupportTypes { get; set; }
    public string SupportNeededReason { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }

    public GetProgressSupportResponse()
    {
        SupportTypes = new List<EProgressReportSupportType>();
    }
}