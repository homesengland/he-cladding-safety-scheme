using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport;

public class GetProgressReportDetailsResult
{
    public Guid Id { get; set; }
    public int ApplicationSchemeId { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateDue { get; set; }
    public DateTime? DateSubmitted { get; set; }

    // KD - Works Planning
    public DateTime? ExpectedTenderDate { get; set; }
    public DateTime? ActualTenderDate { get; set; }
    public DateTime? ExpectedLeadContractorAppointmentDate { get; set; }
    public DateTime? ActualLeadContractAppointmentDate { get; set; }
    public DateTime? ExpectedWorksPackageSubmissionDate { get; set; }
    public EContractorTenderType? ContractorTenderTypeId { get; set; }
    public string ContractorTenderReason { get; set; }
    public EProgressReportKeyDatesChangeType? WorksPlanningChangeTypeId { get; set; }
    public string WorksPlanningChangeReason { get; set; }

    // KD - Building Control
    public DateTime? BuildingControlExpectedApplicationDate { get; set; }
    public DateTime? BuildingControlActualApplicationDate { get; set; }
    public string BuildingControlGateway2Reference { get; set; }
    public DateTime? BuildingControlValidationDate { get; set; }
    public DateTime? BuildingControlDecisionDate { get; set; }
    public EBuildingControlDecisionType? BuildingControlDecisionTypeId { get; set; }
    public string BuildingControlDecisionType { get; set; }
    public string BuildingControlDecisionDocumentsCsv { get; set; }
    public EProgressReportKeyDatesChangeType? BuildingControlChangeTypeId { get; set; }
    public string BuildingControlChangeReason { get; set; }

    // KD - Planning Permission
    public EYesNoNonBoolean? WorksNeedPlanningPermission { get; set; }
    public bool? HaveAppliedPlanningPermission { get; set; }
    public DateTime? PlanningPermissionDateSubmitted { get; set; }
    public DateTime? PlanningPermissionDateApproved { get; set; }
    public DateTime? PlanningPermissionPlanToSubmitDate { get; set; }
    public string PlanningPermissionReasonNotApplied { get; set; }
    public EProgressReportKeyDatesChangeType? PlanningPermissionChangeTypeId { get; set; }
    public string PlanningPermissionChangeReason { get; set; }

    // KD - Remediation
    public DateTime? RemediationFullCompletionOfWorksDate { get; set; } // Expected Start on Site date
    public DateTime? RemediationPracticalCompletionDate { get; set; }
    public EProgressReportKeyDatesChangeType? RemediationChangeTypeId { get; set; }
    public string RemediationChangeReason { get; set; }

    // Project Plan
    public string IntentToProceedType { get; set; }
    public bool? InternalAdditionalWork { get; set; }
    public decimal? RemainingAmount { get; set; }
    public bool? EnoughFunds { get; set; }
    public string ProjectPlanDocumentsCsv { get; set; }
    public string PtsUpliftDocumentsCsv { get; set; }

    // Leaseholder Communication
    public bool? HasContacted { get; set; }
    public DateTime? LastCommunicationDate { get; set; }
    public string LeaseholderCommunicationDocumentsCsv { get; set; }

    // Support
    public bool? RequiresSupport { get; set; }
    public bool? LeadDesignerNeedsSupport { get; set; }
    public bool? OtherMembersNeedsSupport { get; set; }
    public bool? QuotesNeedsSupport { get; set; }
    public bool? PlanningPermissionNeedsSupport { get; set; }
    public bool? OtherNeedsSupport { get; set; }
    public string SupportNeededReason { get; set; }

    // Project Team
    public bool TeamHasMembers { get; set; }
    public bool TeamHasGco { get; set; }

    // Legacy Data
    public bool HasLegacyReport { get; set; }
    public bool? HasBuildingSafetyRegulatorRegistrationCode { get; set; }
    public string BuildingSafetyRegulatorRegistrationCode { get; set; }
    public bool? Is7StoreysOr18Metres { get; set; }
    public bool? HasAppliedForBuildingControl { get; set; }
    public bool? HasProjectPlanMilestones { get; set; }
    public bool? HasSoughtQuotesOrIssuedTender { get; set; }
    public bool? HasAppointedTeamMembers { get; set; }
    public string BuildingControlActualSubmissionInformation { get; set; }
    public string BuildingControlValidationInformation { get; set; }
    public string BuildingControlDecisionInformation { get; set; }
    public string RisksAndBlockersSummary { get; set; }
}