using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Preliminary;

public class GetPreliminaryRequest : IRequest<GetPreliminaryResponse>
{
    private GetPreliminaryRequest()
    {
    }

    public static readonly GetPreliminaryRequest Request = new();
}