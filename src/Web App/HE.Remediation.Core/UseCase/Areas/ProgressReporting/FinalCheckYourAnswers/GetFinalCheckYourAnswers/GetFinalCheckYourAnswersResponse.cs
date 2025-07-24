using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.FinalCheckYourAnswers.GetFinalCheckYourAnswers;

public class GetFinalCheckYourAnswersResponse
{
    public bool? LeaseholdersInformed { get; set; } 
    public string LeaseholdersInformedEvidenceFiles { get; set; }

    public bool? LeadDesignerAppointed { get; set; }

    public bool? OtherMembersAppointed { get; set; }

    public bool? QuotesSought { get; set; }

    public EYesNoNonBoolean? RequirePlanningPermission { get; set; }

    public bool? AppliedForPlanningPermission { get; set; }

    public DateTime? PlanningPermissionPlannedSubmitDate { get; set; }

    public DateTime? PlanningPermissionSubmittedDate { get; set; }

    public DateTime? PlanningPermissionApprovedDate { get; set; }

    public bool ShowBuildingSafetyRegulatorRegistration { get; set; }

    public bool? BuildingHasSafetyRegulatorRegistrationCode { get; set; }

    public string BuildingSafetyRegulatorRegistrationCode { get; set; }

    public string LeadDesignerNotAppointedReason { get; set; }

    public string OtherMembersNotAppointedReason { get; set; }

    public string ReasonPlanningPermissionNotApplied { get; set; }
    public EIntentToProceedType? IntentToProceedType { get; set; }

    public bool? HasProjectPlanMilestones { get; set; }


    public EWhyYouHaveNotSoughtQuotes? WhyYouHaveNotSoughtQuotes { get; set; }

    public string QuotesNotSoughtReason { get; set; }

    public DateTime? ExpectedWorksPackageSubmissionDate { get; set; }

    public DateTime? ExpectedStartDateOnSite { get; set; }

    public bool? LeadDesignerNeedsSupport { get; set; }

    public bool? OtherMembersNeedsSupport { get; set; }

    public bool? PlanningPermissionNeedsSupport { get; set; }

    public bool? QuotesNeedsSupport { get; set; }

    public string SupportNeededReason { get; set; }

    public List<GetTeamMembersResult> TeamMembers { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
    public bool? BuildingControlDetailsRequired { get; set; }
    public bool? HasAppliedForBuildingControl { get; set; }
    public DateTime? BuildingControlForecastDate { get; set; }
    public string BuildingControlForecastInformation { get; set; }
    public DateTime? BuildingControlActualDate { get; set; }
    public string BuildingControlActualInformation { get; set; }
    public string BuildingControlApplicationReference { get; set; }
    public DateTime? BuildingControlValidationDate { get; set; }
    public string BuildingControlValidationInformation { get; set; }
    public DateTime? BuildingControlDecisionDate { get; set; }
    public string BuildingControlDecisionInformation { get; set; }
    public bool? BuildingControlDecision { get; set; }

    public bool? HasGco { get; set; }
    public string TeamMember { get; set; }
    public string Role { get; set; }
    public string NameNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
    public string Signatory { get; set; }
    public string SignatoryEmailAddress { get; set; }
    public DateTime? DateAppointed { get; set; }
}

