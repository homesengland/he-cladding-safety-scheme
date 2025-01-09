using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Costs.Delete;

public class DeleteCostsRequest : IRequest
{
    private DeleteCostsRequest()
    {
    }

    public static DeleteCostsRequest Request => new();
}
