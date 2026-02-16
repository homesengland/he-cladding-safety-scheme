using System.Transactions;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;
public class SetReasonNotAppliedPlanningPermission : IRequestHandler<SetReasonNotAppliedPlanningPermissionRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;
    public SetReasonNotAppliedPlanningPermission(IApplicationDataProvider applicationDataProvider, IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _keyDatesRepository = keyDatesRepository;
    }
    public async ValueTask<Unit> Handle(SetReasonNotAppliedPlanningPermissionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var plannedDateToSubmit = request.PlannedYearToSubmitApplication.HasValue && request.PlannedMonthToSubmitApplication.HasValue 
            ? new DateTime(request.PlannedYearToSubmitApplication.Value, request.PlannedMonthToSubmitApplication.Value, 1) 
            : (DateTime?)null;

        await _keyDatesRepository.SetProgressReportReasonNotAppliedPlanningPermission(new SetProgressReportReasonNotAppliedPlanningPermissionParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            ReasonNotAppliedPlanningPermission = request.ReasonNotAppliedPlanningPermission,
            PlannedDateToSubmitApplication = plannedDateToSubmit
        });

        if (request.PreviousPlanToSubmitDate.HasValue && plannedDateToSubmit.HasValue
                                                      && request.PreviousPlanToSubmitDate == plannedDateToSubmit)
        {
            await _keyDatesRepository.ClearPlanningPermissionSlippage(new ClearPlanningPermissionSlippageParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });
        }

        scope.Complete();
        return Unit.Value;
    }
}

public class SetReasonNotAppliedPlanningPermissionRequest : IRequest<Unit>
{
    public int? PlannedYearToSubmitApplication { get; set; }
    public int? PlannedMonthToSubmitApplication { get; set; }
    public string ReasonNotAppliedPlanningPermission { get; set; }
    public DateTime? PreviousPlanToSubmitDate { get; set; }
}
