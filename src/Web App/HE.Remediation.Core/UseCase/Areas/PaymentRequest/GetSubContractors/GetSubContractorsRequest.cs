using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubContractors;

public class GetSubContractorsRequest : IRequest<GetSubContractorsResponse>
{
    private GetSubContractorsRequest()
    {
    }

    public static readonly GetSubContractorsRequest Request = new();
}
