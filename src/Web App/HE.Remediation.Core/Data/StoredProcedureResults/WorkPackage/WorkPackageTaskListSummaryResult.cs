using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage
{
    public class WorkPackageTaskListSummaryResult
    {
        public ETaskStatus WorkPackageGrantCertifyingOfficerStatusId { get; set; }

        public ETaskStatus WorkPackageCostsScheduleStatusId { get; set; }

        public ETaskStatus WorkPackageThirdPartyContributionsStatusId { get; set; }

        public ETaskStatus WorkPackageDeclarationStatusId { get; set; }

        public ETaskStatus WorkPackageDutyOfCareDeedStatusId { get; set; }

        public bool WorkPackageDutyOfCareDeedSent { get; set; }

        public ETaskStatus WorkPackageProjectTeamStatusId { get; set; }

        public ETaskStatus WorkPackagePlanningPermissionStatusId { get; set; }

        public ETaskStatus WorkPackageKeyDatesStatusId { get; set; }
        
        public ETaskStatus WorkPackageSignatoriesStatusId { get; set; }

        public bool IsSubmitted { get; set; }
    }
}
