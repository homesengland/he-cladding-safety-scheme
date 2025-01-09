
using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WorksRequirePermission.SetWorksRequirePermission;

public class SetWorksRequirePermissionHandler : IRequestHandler<SetWorksRequirePermissionRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetWorksRequirePermissionHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetWorksRequirePermissionRequest request, CancellationToken cancellationToken)
    {
        await UpdateRequirePlanningPermission(request);
        return Unit.Value;
    }

    private async Task UpdateRequirePlanningPermission(SetWorksRequirePermissionRequest request)
    {
        await _progressReportingRepository.UpdateRequirePlanningPermission(request.PermissionRequired);
    }
}
