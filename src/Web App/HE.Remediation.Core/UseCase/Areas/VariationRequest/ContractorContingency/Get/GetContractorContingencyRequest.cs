using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.ContractorContingency.Get;

public class GetContractorContingencyRequest : IRequest<GetContractorContingencyResponse>
{
    private GetContractorContingencyRequest()
    {
    }

    public static GetContractorContingencyRequest Request => new();
}
