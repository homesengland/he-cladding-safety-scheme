using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Details.GetProgressReportDetails;

public class GetProgressReportDetailsResponse
{
    public bool? LeaseholdersInformed { get; set; }
    public bool? LeadDesignerAppointed { get; set; }
    public bool? OtherMembersAppointed { get; set; }
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
    public bool NextReportExists { get; set; }
    public IList<TeamMember> TeamMembers { get; set; }

    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }

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