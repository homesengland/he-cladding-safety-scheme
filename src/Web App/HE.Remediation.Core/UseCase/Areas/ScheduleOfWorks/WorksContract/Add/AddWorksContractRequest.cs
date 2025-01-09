using MediatR;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Add;

public class AddWorksContractRequest : IRequest<Unit>
{
    public IFormFile File { get; set; }
    public bool Completed { get; set; }
}
