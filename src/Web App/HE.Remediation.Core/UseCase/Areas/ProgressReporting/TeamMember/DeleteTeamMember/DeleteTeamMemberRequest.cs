
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.DeleteTeamMember;

public class DeleteTeamMemberRequest : IRequest
{
    public Guid TeamMemberId { get; set; }
}
