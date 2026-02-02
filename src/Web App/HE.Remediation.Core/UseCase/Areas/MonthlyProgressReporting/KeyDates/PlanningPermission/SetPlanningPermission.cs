using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;
public class SetPlanningPermissionHandler : IRequestHandler<SetPlanningPermissionRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public SetPlanningPermissionHandler(IApplicationDataProvider applicationDataProvider, IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async ValueTask<Unit> Handle(SetPlanningPermissionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var planningPermission = await _keyDatesRepository.GetProgressReportPlanningPermissionKeyDates(new GetProgressReportPlanningPermissionKeyDatesParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
        await _keyDatesRepository.SetProgressReportPlanningPermissionKeyDates(new SetProgressReportPlanningPermissionKeyDatesParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            WorksNeedPlanningPermission = request.WorksNeedPlanningPermission
        });

        return Unit.Value;
    }
}

public class SetPlanningPermissionRequest : IRequest<Unit>
{
    public int WorksNeedPlanningPermission { get; set; }
}
