using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ApprovedScheduleOfWorks.CostProfile.Get;

public class GetCostProfileRequest : IRequest<GetCostProfileResponse>
{
    private GetCostProfileRequest()
    {
    }

    public static GetCostProfileRequest Request => new();
}
