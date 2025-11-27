using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.TeamMember;
public class DeleteTeamMemberRequest : IRequest
{
    public Guid TeamMemberId { get; set; }
}