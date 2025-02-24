using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Costs.Create;

public class CreateCostsRequest : IRequest
{
    private CreateCostsRequest()
    {
    }

    public static CreateCostsRequest Request => new();
}
