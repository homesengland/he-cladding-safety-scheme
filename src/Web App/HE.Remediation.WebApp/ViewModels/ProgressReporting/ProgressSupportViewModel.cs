using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ProgressSupportViewModel
{
    public IList<EProgressReportSupportType> SupportTypes { get; set; }

    public string SupportNeededReason { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }

    public ESubmitAction SubmitAction { get; set; }

    public ProgressSupportViewModel()
    {
        SupportTypes = new List<EProgressReportSupportType>();
    }
}