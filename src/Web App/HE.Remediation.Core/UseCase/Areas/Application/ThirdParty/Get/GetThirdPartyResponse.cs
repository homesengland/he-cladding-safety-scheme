using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Get;

public class GetThirdPartyResponse
{
    public List<TeamMember> TeamMembers { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

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
