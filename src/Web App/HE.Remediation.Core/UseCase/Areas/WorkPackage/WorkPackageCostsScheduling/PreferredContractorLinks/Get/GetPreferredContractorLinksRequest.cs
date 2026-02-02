using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.PreferredContractorLinks.Get;

public class GetPreferredContractorLinksRequest : IRequest<GetPreferredContractorLinksResponse>
{
    private GetPreferredContractorLinksRequest()
    {
    }

    public static GetPreferredContractorLinksRequest Request => new();
}
