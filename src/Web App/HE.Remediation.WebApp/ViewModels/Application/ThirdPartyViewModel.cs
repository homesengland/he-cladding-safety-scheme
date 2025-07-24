using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class ThirdPartyViewModel
    {
        public string BuildingName { get; set; }
        public string ApplicationReferenceNumber { get; set; }
        public List<TeamMember> TeamMembers { get; set; }

        public class TeamMember
        {
            public Guid Id { get; set; }
            public string RoleName { get; set; }
            public string Name { get; set; }
            public string EmailAddress { get; set; }
            public ECollaborationUserStatus? InviteStatus { get; set; }
            public ETeamMemberSource Source { get; set; }
        }
    }
}
