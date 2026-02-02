using Mediator;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Set;

public class SetWorksContractRequest : IRequest<Unit>
{
    public IFormFile File { get; set; }
    public bool Completed { get; set; }
}
