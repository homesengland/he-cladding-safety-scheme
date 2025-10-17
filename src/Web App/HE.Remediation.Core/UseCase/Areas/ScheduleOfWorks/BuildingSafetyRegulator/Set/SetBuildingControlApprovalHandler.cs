using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingSafetyRegulator.Get;

using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingSafetyRegulator.Set;

public class SetBuildingControlApprovalHandler : IRequestHandler<SetBuildingControlApprovalRequest>
{
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public SetBuildingControlApprovalHandler(IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async Task<Unit> Handle(SetBuildingControlApprovalRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _scheduleOfWorksRepository.SetScheduleOfWorksBuildingControlApprovalApplied(request.IsBuildingControlApprovalApplied);

        return Unit.Value;
    }
}