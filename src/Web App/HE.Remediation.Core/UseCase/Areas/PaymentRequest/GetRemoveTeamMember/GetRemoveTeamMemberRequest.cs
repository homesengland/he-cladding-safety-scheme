using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetRemoveTeamMember;

public class GetRemoveTeamMemberRequest : IRequest<GetRemoveTeamMemberResponse>
{
    public Guid TeamMemberId { get; set; }

    public GetRemoveTeamMemberRequest()
    {
    }
}
