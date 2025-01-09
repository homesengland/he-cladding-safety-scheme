using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetVariationRequired;

public class GetVariationRequiredRequest : IRequest<GetVariationRequiredResponse>
{
    private GetVariationRequiredRequest()
    {
    }

    public static readonly GetVariationRequiredRequest Request = new();
}
