using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.ScheduleOfWorks
{
    public class TaskStatusesResult
    {
        public ETaskStatus? LeaseholderEngagementStatusId { get; set; }

        public ETaskStatus? BuildingControlStatusId { get; set; }

        public ETaskStatus? WorksContractStatusId { get; set; }

        public ETaskStatus? ProfileCostsStatusId { get; set; }

        public ETaskStatus? DeclarationStatusId { get; set; }
    }
}
