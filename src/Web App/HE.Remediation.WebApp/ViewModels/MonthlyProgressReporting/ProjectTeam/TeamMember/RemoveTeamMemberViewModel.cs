using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.TeamMember;
public class RemoveTeamMemberViewModel
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public Guid TeamMemberId { get; set; }

    public string TeamMemberName { get; set; }
    public ETeamRole Role { get; set; }

    public bool? Confirm { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
