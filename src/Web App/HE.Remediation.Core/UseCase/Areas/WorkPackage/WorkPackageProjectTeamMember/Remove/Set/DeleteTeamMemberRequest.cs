
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.Remove.Set;

public class DeleteTeamMemberRequest : IRequest
{
    public Guid TeamMemberId { get; set; }
}
