using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting;

public class ProgressReportDetailsViewModel
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }

    public Guid Id { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateDue { get; set; }
    public DateTime? DateSubmitted { get; set; }

    public bool NextReportExists { get; set; }
    public bool BuildingControlRequired { get; set; }

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
    public List<string> BuildingControlDecisionDocuments { get; set; }
    public EProgressReportKeyDatesChangeType? BuildingControlChangeTypeId { get; set; }
    public string BuildingControlChangeReason { get; set; }

    // KD - Planning Permission
    public bool? WorksNeedPlanningPermission { get; set; }
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
    public List<string> ProjectPlanDocuments { get; set; }
    public List<string> PtsUpliftDocuments { get; set; }

    // Leaseholder Communication
    public bool? HasContacted { get; set; }
    public DateTime? LastCommunicationDate { get; set; }
    public List<string> LeaseholderCommunicationDocuments { get; set; }

    // Support
    public bool? RequiresSupport { get; set; }
    public bool? LeadDesignerNeedsSupport { get; set; }
    public bool? OtherMembersNeedsSupport { get; set; }
    public bool? QuotesNeedsSupport { get; set; }
    public bool? PlanningPermissionNeedsSupport { get; set; }
    public bool? OtherNeedsSupport { get; set; }
    public string SupportNeededReason { get; set; }

    public List<string> DisplaySupportNeeds()
    {
        var items = new List<string>();
        if (LeadDesignerNeedsSupport.GetValueOrDefault()) items.Add("Appointing Designer");
        if (OtherMembersNeedsSupport.GetValueOrDefault()) items.Add("Appointing Team");
        if (QuotesNeedsSupport.GetValueOrDefault()) items.Add("Seeking Quotes");
        if (PlanningPermissionNeedsSupport.GetValueOrDefault()) items.Add("Planning Permission");
        if (OtherNeedsSupport.GetValueOrDefault()) items.Add("Other");
        return items;
    }

    // Project Team

    public IList<TeamMember> TeamMembers { get; set; } = [];
    public string GcoName { get; set; }
    public string GcoCompanyName { get; set; }
    public string GcoNameNumber { get; set; }
    public string GcoAddressLine1 { get; set; }
    public string GcoAddressLine2 { get; set; }
    public string GcoCity { get; set; }
    public string GcoCounty { get; set; }
    public string GcoPostCode { get; set; }
    public string GcoAuthorisedSignatory { get; set; }
    public string GcoAuthorisedSignatoryEmailAddress { get; set; }
    public DateTime? GcoContractStartDate { get; set; }

    public KeyRoleTeamMember ApplicationLead { get; set; }
    public KeyRoleTeamMember LeaseholderCommunicator { get; set; }
    public KeyRoleTeamMember RegulatoryComplianceMember { get; set; }

    public class TeamMember
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string OtherRole { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
    }

    public class KeyRoleTeamMember
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
    }

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