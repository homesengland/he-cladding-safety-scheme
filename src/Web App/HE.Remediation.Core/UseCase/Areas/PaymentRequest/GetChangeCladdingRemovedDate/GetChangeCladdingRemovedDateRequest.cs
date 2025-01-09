using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetChangeCladdingRemovedDate;

public class GetChangeCladdingRemovedDateRequest : IRequest<GetChangeCladdingRemovedDateResponse>
{
    private GetChangeCladdingRemovedDateRequest()
    {
    }

    public static readonly GetChangeCladdingRemovedDateRequest Request = new();
}
