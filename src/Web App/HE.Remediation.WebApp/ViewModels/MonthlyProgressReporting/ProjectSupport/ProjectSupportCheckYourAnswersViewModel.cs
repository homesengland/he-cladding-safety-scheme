using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectSupport;
public class ProjectSupportCheckYourAnswersViewModel
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public bool? RequiresSupport { get; set; }
    public IList<EProgressReportSupportType> SupportTypes { get; set; }
    public string SupportNeededReason { get; set; }
    public ProjectSupportCheckYourAnswersViewModel()
    {
        SupportTypes = new List<EProgressReportSupportType>();
    }
}
