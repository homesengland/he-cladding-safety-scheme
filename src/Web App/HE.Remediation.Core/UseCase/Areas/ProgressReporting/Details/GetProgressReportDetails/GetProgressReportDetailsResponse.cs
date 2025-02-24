using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Details.GetProgressReportDetails;

public class GetProgressReportDetailsResponse
{
    public bool? LeaseholdersInformed { get; set; }
    public bool? LeadDesignerAppointed { get; set; }
    public bool? OtherMembersAppointed { get; set; }
    public EIntentToProceedType? IntentToProceedType { get; set; }
    public bool? QuotesSought { get; set; }
    public EWhyYouHaveNotSoughtQuotes? WhyYouHaveNotSoughtQuotes { get; set; }
    public string QuotesNotSoughtReason { get; set; }
    public int? RequirePlanningPermission { get; set; }
    public bool? AppliedForPlanningPermission { get; set; }
    public string ReasonPlanningPermissionNotApplied { get; set; }
    public DateTime? ExpectedWorksPackageSubmissionDate { get; set; }
    public bool? SpentAnyFunding { get; set; }
    public DateTime? DateSubmitted { get; set; }
    public DateTime DateCreated { get; set; }
    public bool? BuildingControlRequired { get; set; }
    public bool? HasAppliedForBuildingControl { get; set; }
    public DateTime? BuildingControlForecastSubmissionDate { get; set; }
    public string BuildingControlForecastInformation { get; set; }
    public DateTime? BuildingControlActualSubmissionDate { get; set; }
    public string BuildingControlActualSubmissionInformation { get; set; }
    public string BuildingControlApplicationReference { get; set; }
    public DateTime? BuildingControlValidationDate { get; set; }
    public string BuildingControlValidationInformation { get; set; }
    public DateTime? BuildingControlDecisionDate { get; set; }
    public string BuildingControlDecisionInformation { get; set; }
    public bool? BuildingControlDecision { get; set; }
    public DateTime? ExpectedStartDateOnSite { get; set; }
    public bool NextReportExists { get; set; }
    public IList<TeamMember> TeamMembers { get; set; }

    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public int version { get; set; }

    public string ProgressSummary { get; set; }
    public string GoalSummary { get; set; }
    public bool? IsSupportNeeded { get; set; }

    public GetProgressReportDetailsResponse()
    {
        TeamMembers = new List<TeamMember>();
    }

    public class TeamMember
    {
        public Guid Id { get; set; }
        public ETeamRole Role { get; set; }
        public string OtherRole { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
    }
}