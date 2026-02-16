using System.Transactions;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;
public class SetHaveYouAppliedPlanningPermissionHandler : IRequestHandler<SetHaveYouAppliedPlanningPermissionRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public SetHaveYouAppliedPlanningPermissionHandler(IApplicationDataProvider applicationDataProvider, IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async ValueTask<Unit> Handle(SetHaveYouAppliedPlanningPermissionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var haveYouAppliedPlanningPermission = await _keyDatesRepository.GetProgressReportHaveYouAppliedPlanningPermission(new GetProgressReportHaveYouAppliedPlanningPermissionParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        if (haveYouAppliedPlanningPermission.HasValue &&
            haveYouAppliedPlanningPermission != request.HaveAppliedPlanningPermission!.Value)
        {
            await _keyDatesRepository.ClearPlanningPermissionSlippage(new ClearPlanningPermissionSlippageParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });
        }

        await _keyDatesRepository.SetProgressReportHaveYouAppliedPlanningPermission(new SetProgressReportHaveYouAppliedPlanningPermissionParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            HaveAppliedPlanningPermission = request.HaveAppliedPlanningPermission!.Value
        });

        scope.Complete();

        return Unit.Value;
    }
}

public class SetHaveYouAppliedPlanningPermissionRequest : IRequest<Unit>
{
    public bool? HaveAppliedPlanningPermission { get; set; }
}