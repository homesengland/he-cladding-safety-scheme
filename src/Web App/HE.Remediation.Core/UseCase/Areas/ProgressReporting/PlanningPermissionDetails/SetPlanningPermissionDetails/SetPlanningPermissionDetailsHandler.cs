using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionDetails.SetPlanningPermissionDetails
{
    public class SetPlanningPermissionDetailsHandler : IRequestHandler<SetPlanningPermissionDetailsRequest, Unit>
    {
        private readonly IProgressReportingRepository _progressReportingRepository;

        public SetPlanningPermissionDetailsHandler(IProgressReportingRepository progressReportingRepository)
        {
            _progressReportingRepository = progressReportingRepository;
        }

        public async Task<Unit> Handle(SetPlanningPermissionDetailsRequest request, CancellationToken cancellationToken)
        {
            await UpdateProgressReportPlanningPermissionDetails(request);

            return Unit.Value;
        }

        private async Task UpdateProgressReportPlanningPermissionDetails(SetPlanningPermissionDetailsRequest request)
        {
            var planningPermissionSubmittedDate = request.PlanningPermissionSubmittedMonth is not null && request.PlanningPermissionSubmittedYear is not null
                ? new DateTime(request.PlanningPermissionSubmittedYear.Value, request.PlanningPermissionSubmittedMonth.Value, 1)
                : (DateTime?)null;

            var planningPermissionApprovedDate = request.PlanningPermissionApprovedMonth is not null && request.PlanningPermissionApprovedYear is not null
                ? new DateTime(request.PlanningPermissionApprovedYear.Value, request.PlanningPermissionApprovedMonth.Value, 1)
                : (DateTime?)null;

            await _progressReportingRepository.UpdateProgressReportPlanningPermissionDetails(planningPermissionSubmittedDate, planningPermissionApprovedDate);
        }
    }
}