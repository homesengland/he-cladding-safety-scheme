using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.InstallationOfCladdingCosts.Get;

public class GetInstallationOfCladdingCostsRequest : IRequest<GetInstallationOfCladdingCostsResponse>
{
    private GetInstallationOfCladdingCostsRequest()
    {
    }

    public static GetInstallationOfCladdingCostsRequest Request => new();
}
