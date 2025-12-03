using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectTeam;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.ExistingTeamMember;
public class ExistingTeamMemberViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool? SameAsPrevious { get; set; }

    public Guid? SelectedPreviousTeamMember { get; set; }

    public ETeamRole? TeamRole { get; set; }

    public List<GetTeamMembersResult> TeamMembers { get; set; }

    public ESubmitAction SubmitAction { get; set; }

    public string ReturnUrl { get; set; }
}