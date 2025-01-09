using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.WorksContract.Delete;

public class DeleteWorksContractRequest : IRequest
{
    public Guid FileId { get; set; }
}

