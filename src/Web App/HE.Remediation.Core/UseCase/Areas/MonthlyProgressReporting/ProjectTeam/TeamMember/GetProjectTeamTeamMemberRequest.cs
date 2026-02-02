using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.TeamMember;
public class GetProjectTeamTeamMemberRequest : IRequest<GetProjectTeamTeamMemberResponse>
{
    public Guid? TeamMemberId { get; set; }
    public ETeamRole TeamRole { get; set; }
    public Guid? Selected { get; set; }
}
