using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting;

public class TaskListItemViewModel
{
    public string Task { get; set; }
    public string TaskUrl { get; set; }
    public ETaskStatus? Status { get; set; }
    public bool IsReportSubmitted { get; set; }
}