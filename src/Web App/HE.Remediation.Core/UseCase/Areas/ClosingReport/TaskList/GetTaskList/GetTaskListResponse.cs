using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.TaskList.GetTaskList;

public class GetTaskListResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }

    public IReadOnlyCollection<TaskWithStatus> TasksWithStatuses { get; set; }

    public class TaskWithStatus(EClosingReportTask task, ETaskStatus status)
    {
        public EClosingReportTask Task { get; set; } = task;
        public ETaskStatus Status { get; set; } = status;
    }
}
