using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class WhoIsTheGrantCertifyingOfficerViewModel
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string ReturnUrl { get; set; }

    public Guid? ProjectTeamMemberId { get; set; }
    public IList<TeamMemberViewModel> TeamMembers { get; set; }

    public int Version { get; set; }
    public bool IsGcoComplete { get; set; }

    public ESubmitAction SubmitAction { get; set; }

    public class TeamMemberViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}