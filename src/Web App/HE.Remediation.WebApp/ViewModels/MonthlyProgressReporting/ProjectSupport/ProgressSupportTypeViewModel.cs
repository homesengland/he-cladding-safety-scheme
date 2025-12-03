using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectSupport;

public class ProgressSupportTypeViewModel
{
    public IList<EProgressReportSupportType> SupportTypes { get; set; }

    public string SupportNeededReason { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public ESubmitAction SubmitAction { get; set; }

    public ProgressSupportTypeViewModel()
    {
        SupportTypes = new List<EProgressReportSupportType>();
    }
}