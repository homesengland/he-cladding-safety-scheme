using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting
{
    public class TaskListViewModel
    {
        public string BuildingName { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDue { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public int Version { get; set; }
        public ETaskStatus TaskStatusId { get; set; }

        public ETaskStatus KeyDatesTaskStatusId { get; set; }
        public ETaskStatus ProjectTeamTaskStatusId { get; set; }
        public ETaskStatus ProjectPlanTaskStatusId { get; set; }
        public ETaskStatus LeaseholdersTaskStatusId { get; set; }
        public ETaskStatus SupportTaskStatusId { get; set; }

        public bool IsSubmitActive()
        {
            return KeyDatesTaskStatusId == ETaskStatus.Completed &&
                ProjectTeamTaskStatusId == ETaskStatus.Completed &&
                ProjectPlanTaskStatusId == ETaskStatus.Completed &&
                LeaseholdersTaskStatusId == ETaskStatus.Completed &&
                SupportTaskStatusId == ETaskStatus.Completed;
        }
    }
}
