
using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonPlanningNotApplied.SetReasonPlanningNotApplied;

public class SetReasonPlanningNotAppliedHandler : IRequestHandler<SetReasonPlanningNotAppliedRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetReasonPlanningNotAppliedHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetReasonPlanningNotAppliedRequest request, CancellationToken cancellationToken)
    {
        await UpdatePlanningPermissionNotAppliedReason(request);
        return Unit.Value;
    }

    private async Task UpdatePlanningPermissionNotAppliedReason(SetReasonPlanningNotAppliedRequest request)
    {
        await _progressReportingRepository.UpdatePlanningPermissionNotAppliedReason(request.ReasonPlanningPermissionNotApplied, request.PlanningPermissionNeedsSupport);
    }
}
