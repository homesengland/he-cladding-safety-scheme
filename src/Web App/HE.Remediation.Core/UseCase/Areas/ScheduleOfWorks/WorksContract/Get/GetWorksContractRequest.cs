using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Get;

public class GetWorksContractRequest : IRequest<GetWorksContractResponse>
{
    private GetWorksContractRequest()
    {
    }

    public static GetWorksContractRequest Request => new();
}
