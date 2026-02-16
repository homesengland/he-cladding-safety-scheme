using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;

public class SetPlanningPermissionDatesChangedHandler : IRequestHandler<SetPlanningPermissionDatesChangedRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public SetPlanningPermissionDatesChangedHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async ValueTask<Unit> Handle(SetPlanningPermissionDatesChangedRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        await _keyDatesRepository.SetPlanningPermissionDatesChanged(new SetPlanningPermissionDatesChangedParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            DatesChangedTypeId = request.ChangeTypeId!.Value,
            DatesChangedReason = request.ChangeReason
        });

        return Unit.Value;
    }
}

public class SetPlanningPermissionDatesChangedRequest : IRequest
{
    public int? ChangeTypeId { get; set; }
    public string ChangeReason { get; set; }
}