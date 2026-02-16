using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Description;

public class GetCostDescriptionRequest : IRequest<GetCostDescriptionResponse>
{
    private GetCostDescriptionRequest()
    {
    }

    public static readonly GetCostDescriptionRequest Request = new();
}