using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.DeleteTeamMember;

public class DeleteTeamMemberRequest : IRequest
{
    public Guid TeamMemberId { get; set; }
}
