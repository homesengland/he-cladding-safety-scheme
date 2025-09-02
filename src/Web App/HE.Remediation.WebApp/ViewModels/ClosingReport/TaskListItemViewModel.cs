using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class TaskListItemViewModel
{
    public EClosingReportTask Task { get; set; }
    public string TaskUrl { get; set; }
    public ETaskStatus? Status { get; set; }
}