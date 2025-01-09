using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionPlannedSubmitDate.SetPlanningPermissionPlannedSubmitDate
{
    public class SetPlanningPermissionPlannedSubmitDateHandler : IRequestHandler<SetPlanningPermissionPlannedSubmitDateRequest, Unit>
    {
        private readonly IProgressReportingRepository _progressReportingRepository;

        public SetPlanningPermissionPlannedSubmitDateHandler(IProgressReportingRepository progressReportingRepository)
        {
            _progressReportingRepository = progressReportingRepository;
        }

        public async Task<Unit> Handle(SetPlanningPermissionPlannedSubmitDateRequest request, CancellationToken cancellationToken)
        {
            await UpdateProgressReportPlanningPermissionPlannedSubmitDate(request);

            return Unit.Value;
        }

        private async Task UpdateProgressReportPlanningPermissionPlannedSubmitDate(SetPlanningPermissionPlannedSubmitDateRequest request)
        {
            var planningPermissionPlannedSubmitDate = request.PlanningPermissionPlannedSubmitMonth is not null && request.PlanningPermissionPlannedSubmitYear is not null
                ? new DateTime(request.PlanningPermissionPlannedSubmitYear.Value, request.PlanningPermissionPlannedSubmitMonth.Value, 1)
                : (DateTime?)null;

            await _progressReportingRepository.UpdateProgressReportPlanningPermissionPlannedSubmitDate(planningPermissionPlannedSubmitDate);
        }
    }
}