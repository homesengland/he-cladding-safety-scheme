using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.ClosingReport.Shared;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class TaskListViewModel : ClosingReportBaseViewModel
{
    private readonly List<EClosingReportTask> _section1Tasks =
    [
        EClosingReportTask.FireRiskAssessment,
        EClosingReportTask.PracticalCompletionCertificate,
        EClosingReportTask.BuildingControlEvidence,
        EClosingReportTask.CommunicationWithLeaseholders,
        EClosingReportTask.BuildingInsuranceInformation,
        EClosingReportTask.EvidenceOfThirdPartyContribution,
        EClosingReportTask.RatingsForContractors
    ];

    private readonly List<EClosingReportTask> _section2Tasks =
    [
        EClosingReportTask.SubmitPaymentRequest,
        EClosingReportTask.UploadFinalCostReport
    ];

    public IReadOnlyCollection<TaskWithStatus> TasksWithStatuses { get; set; }

    public ETaskStatus? DisplayStatus(EClosingReportTask task)
    {
        // section 1

        if (_section1Tasks.Any(t => t == task))
        {
            return GetStatus(task);
        }

        // section 2

        switch (task)
        {
            case EClosingReportTask.SubmitPaymentRequest:
                return IsComplete(_section1Tasks) ? GetStatus(task) : null;

            case EClosingReportTask.UploadFinalCostReport:
                var isPreviousTaskComplete = GetStatus(EClosingReportTask.SubmitPaymentRequest) == ETaskStatus.Completed;
                return IsComplete(_section1Tasks) && isPreviousTaskComplete ? GetStatus(task) : null;
        }

        // section 3

        if (task == EClosingReportTask.FinalPaymentDeclaration)
        {
            return IsComplete(_section2Tasks) ? GetStatus(task) : null;
        }

        return null;
    }

    private ETaskStatus GetStatus(EClosingReportTask task)
    {
        var status = TasksWithStatuses.FirstOrDefault(t => t.Task == task);
        return status != null ? status.Status : ETaskStatus.NotStarted;
    }

    private bool IsComplete(List<EClosingReportTask> tasks)
    {
        var completeTasks = TasksWithStatuses.Where(t => t.Status == ETaskStatus.Completed).Select(t => t.Task).ToList();
        bool allTasksComplete = tasks.All(t => completeTasks.Contains(t));
        return allTasksComplete;
    }

    public class TaskWithStatus(EClosingReportTask task, ETaskStatus status)
    {
        public EClosingReportTask Task { get; set; } = task;
        public ETaskStatus Status { get; set; } = status;
    }
}