
using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppliedForPlanningPermission.SetAppliedForPlanningPermission;

public class SetAppliedForPlanningPermissionHandler : IRequestHandler<SetAppliedForPlanningPermissionRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetAppliedForPlanningPermissionHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetAppliedForPlanningPermissionRequest request, CancellationToken cancellationToken)
    {
        await _progressReportingRepository.UpdateAppliedForPlanningPermission(request.AppliedForPlanningPermission);
        return Unit.Value;
    }
}
