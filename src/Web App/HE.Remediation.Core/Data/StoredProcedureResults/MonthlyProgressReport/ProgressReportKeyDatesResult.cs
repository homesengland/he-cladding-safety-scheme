using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport
{
    public class ProgressReportKeyDatesResult
    {
        // Works Planning
        public DateTime? ExpectedTenderDate { get; set; }
        public DateTime? ExpectedLeadContractorAppointmentDate { get; set; }
        public DateTime? ActualTenderDate { get; set; }
        public DateTime? ActualLeadContractAppointmentDate { get; set; }
        public DateTime? ExpectedWorksPackageSubmissionDate { get; set; }
        public string WorksPlanningChangeType { get; set; }
        public string WorksPlanningChangeReason { get; set; }
        public EContractorTenderType? ContractorTenderTypeId { get; set; }
        public string ContractorTenderReason { get; set; }

        public DateTime? PreviousExpectedTenderDate { get; set; }
        public DateTime? PreviousActualTenderDate { get; set; }
        public DateTime? PreviousExpectedLeadContractorAppointmentDate { get; set; }
        public DateTime? PreviousActualLeadContractorAppointmentDate { get; set; }
        public DateTime? PreviousExpectedWorksPackageSubmissionDate { get; set; }

        // Building Control
        public DateTime? BuildingControlExpectedApplicationDate { get; set; }
        public DateTime? BuildingControlActualApplicationDate { get; set; }
        public string BuildingControlGateway2Reference { get; set; }
        public DateTime? BuildingControlValidationDate { get; set; }
        public DateTime? BuildingControlDecisionDate { get; set; }
        public EBuildingControlDecisionType? BuildingControlDecision { get; set; }
        public IList<FileResult> BuildingControlDecisionFiles { get; set; } = new List<FileResult>();
        public string BuildingControlChangeType { get; set; }
        public string BuildingControlChangeReason { get; set; }

        public DateTime? PreviousBuildingControlExpectedApplicationDate { get; set; }
        public DateTime? PreviousBuildingControlActualApplicationDate { get; set; }
        public DateTime? PreviousBuildingControlValidationDate { get; set; }
        public DateTime? PreviousBuildingControlDecisionDate { get; set; }

        // Planning Permission
        public int? WorksNeedPlanningPermission { get; set; }
        public bool? HaveAppliedPlanningPermission { get; set; }
        public DateTime? PlanningPermissionDateSubmitted { get; set; }
        public DateTime? PlanningPermissionDateApproved { get; set; }
        public string PlanningPermissionReasonNotApplied { get; set; }
        public DateTime? PlanningPermissionPlanToSubmitDate { get; set; }
        public string PlanningPermissionChangeType { get; set; }
        public string PlanningPermissionChangeReason { get; set; }

        public DateTime? PreviousPlanningPermissionDateSubmitted { get; set; }
        public DateTime? PreviousPlanningPermissionDateApproved { get; set; }
        public DateTime? PreviousPlanningPermissionPlanToSubmitDate { get; set; }

        // Remediation
        public DateTime? RemediationFullCompletionOfWorksDate { get; set; }
        public DateTime? RemediationPracticalCompletionDate { get; set; }
        public string RemediationChangeType { get; set; }
        public string RemediationChangeReason { get; set; }

        public DateTime? PreviousRemediationFullCompletionOfWorksDate { get; set; }
        public DateTime? PreviousRemediationPracticalCompletionDate { get; set; }
    }
}
