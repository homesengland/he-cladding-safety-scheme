using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCladdingRemoved;

public class GetCladdingRemovedRequest : IRequest<GetCladdingRemovedResponse>
{
    private GetCladdingRemovedRequest()
    {
    }

    public static readonly GetCladdingRemovedRequest Request = new();
}
