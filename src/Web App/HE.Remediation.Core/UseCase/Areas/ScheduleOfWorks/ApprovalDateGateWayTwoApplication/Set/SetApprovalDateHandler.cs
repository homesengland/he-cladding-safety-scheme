using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ApprovalDateGateWayTwoApplication.Get;

using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ApprovalDateGateWayTwoApplication.Set;

public class SetApprovalDateHandler : IRequestHandler<SetApprovalDateRequest>
{
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public SetApprovalDateHandler(IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async Task<Unit> Handle(SetApprovalDateRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var approvalDate = new DateTime(request.ApprovalDateYear!.Value, request.ApprovalDateMonth!.Value, request.ApprovalDateDay!.Value);
        await _scheduleOfWorksRepository.SetScheduleOfWorksBuildingControlApprovalDate(approvalDate);

        return Unit.Value;
    }
}