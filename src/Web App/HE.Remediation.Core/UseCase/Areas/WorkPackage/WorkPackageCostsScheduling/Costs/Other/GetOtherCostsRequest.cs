using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Other;

public class GetOtherCostsRequest : IRequest<GetOtherCostsResponse>
{
    private GetOtherCostsRequest()
    {
    }

    public static readonly GetOtherCostsRequest Request = new();
}