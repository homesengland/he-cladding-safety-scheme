using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.Remove.Get;

public class GetRemoveTeamMemberRequest : IRequest<GetRemoveTeamMemberResponse>
{
    public Guid TeamMemberId { get; set; }

    public GetRemoveTeamMemberRequest()
    {
    }
}
