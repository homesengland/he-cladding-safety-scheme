using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectSupport;

public class GetProgressSupportTypeResponse
{
    public IList<EProgressReportSupportType> SupportTypes { get; set; }
    public string SupportNeededReason { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }

    public GetProgressSupportTypeResponse()
    {
        SupportTypes = new List<EProgressReportSupportType>();
    }
}