using HE.Remediation.Core.Enums;


namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.TaskList.GetTaskList
{
    public class GetTaskListResponse
    {
        public string ApplicationReferenceNumber { get; set; }

        public string BuildingName { get; set; }

        public ETaskStatus LeaseholderEngagementStatusId { get; set; }

        public ETaskStatus BuildingControlStatusId { get; set; }

        public ETaskStatus WorksContractStatusId { get; set; }

        public ETaskStatus ProfileCostsStatusId { get; set; }

        public ETaskStatus DeclarationStatusId { get; set; }
    }
}