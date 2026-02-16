using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetTeamMember;

public class GetTeamMemberRequest : IRequest<GetTeamMemberResponse>
{
    public bool? CostsChanged { get; set; }
    public bool? UnsafeCladdingRemoved { get; set; } 

    public Guid? TeamMemberId { get; set; }
    public ETeamRole TeamRole { get; set; }
}
