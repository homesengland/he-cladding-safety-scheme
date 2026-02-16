
using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WorksRequirePermission.SetWorksRequirePermission;

public class SetWorksRequirePermissionHandler : IRequestHandler<SetWorksRequirePermissionRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetWorksRequirePermissionHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetWorksRequirePermissionRequest request, CancellationToken cancellationToken)
    {
        await _progressReportingRepository.UpdateRequirePlanningPermission(request.PermissionRequired);
        return Unit.Value;
    }
}
