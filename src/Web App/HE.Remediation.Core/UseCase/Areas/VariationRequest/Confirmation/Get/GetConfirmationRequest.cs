using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Confirmation.Get;

public class GetConfirmationRequest : IRequest<GetConfirmationResponse>
{
    private GetConfirmationRequest()
    {
    }

    public static GetConfirmationRequest Request => new();
}
