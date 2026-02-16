using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.BaseInformation.Get;

public class GetBaseInformationRequest : IRequest<GetBaseInformationResponse>
{
    private GetBaseInformationRequest()
    {
    }

    public static GetBaseInformationRequest Request => new();
}
