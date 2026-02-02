using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoOtherMembers.GetReasonNoOtherMembers;

public class GetReasonNoOtherMembersRequest : IRequest<GetReasonNoOtherMembersResponse>
{
    private GetReasonNoOtherMembersRequest()
    {
    }

    public static readonly GetReasonNoOtherMembersRequest Request = new();
}
