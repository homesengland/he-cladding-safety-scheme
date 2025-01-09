using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCostsChanged;

public class GetCostsChangedRequest : IRequest<GetCostsChangedResponse>
{
    private GetCostsChangedRequest()
    {
    }

    public static readonly GetCostsChangedRequest Request = new();
}
