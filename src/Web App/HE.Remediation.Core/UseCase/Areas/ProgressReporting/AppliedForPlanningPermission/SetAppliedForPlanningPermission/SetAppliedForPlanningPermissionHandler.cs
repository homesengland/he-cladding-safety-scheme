
using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppliedForPlanningPermission.SetAppliedForPlanningPermission;

public class SetAppliedForPlanningPermissionHandler : IRequestHandler<SetAppliedForPlanningPermissionRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetAppliedForPlanningPermissionHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetAppliedForPlanningPermissionRequest request, CancellationToken cancellationToken)
    {
        await UpdateAppliedForPlanningPermission(request);
        return Unit.Value;
    }

    private async Task UpdateAppliedForPlanningPermission(SetAppliedForPlanningPermissionRequest request)
    {
        await _progressReportingRepository.UpdateAppliedForPlanningPermission(request.AppliedForPlanningPermission);
    }
}
