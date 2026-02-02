using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectSupport;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;
public class SetTellUsAboutPlanningPermission : IRequestHandler<SetTellUsAboutPlanningPermissionRequest, SetTellUsAboutPlanningPermissionResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public SetTellUsAboutPlanningPermission(IApplicationDataProvider applicationDataProvider, IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _keyDatesRepository = keyDatesRepository;
    }
    public async ValueTask<SetTellUsAboutPlanningPermissionResponse> Handle(SetTellUsAboutPlanningPermissionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var keyDates = await _keyDatesRepository.GetProgressReportTellUsAboutPlanningPermission(new GetProgressReportTellUsAboutPlanningPermissionParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _keyDatesRepository.SetProgressReportTellUsAboutPlanningPermission(new SetProgressReportTellUsAboutPlanningPermissionParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            PlanningPermissionDateSubmitted = request.PlanningPermissionDateSubmitted,
            PlanningPermissionDateApproved = request.PlanningPermissionDateApproved
        });

        var planningPermissionDateSubmittedChanged = keyDates.PreviousPlanningPermissionDateSubmitted.HasChanged(request.PlanningPermissionDateSubmitted);
        var planningPermissionDateApprovedChanged = keyDates.PreviousPlanningPermissionDateApproved.HasChanged(request.PlanningPermissionDateApproved);

        var hasChangedDates = planningPermissionDateSubmittedChanged ||
                              planningPermissionDateApprovedChanged;

        if (!hasChangedDates)
        {
            await _keyDatesRepository.ClearPlanningPermissionSlippage(new ClearPlanningPermissionSlippageParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });
        }

        scope.Complete();

        return new SetTellUsAboutPlanningPermissionResponse
        {
            HasChangedDates = hasChangedDates
        };
    }
}

public class SetTellUsAboutPlanningPermissionRequest : IRequest<SetTellUsAboutPlanningPermissionResponse>
{
    public int? PlanningPermissionDateSubmittedMonth { get; set; }
    public int? PlanningPermissionDateSubmittedYear { get; set; }
    public int? PlanningPermissionDateApprovedMonth { get; set; }
    public int? PlanningPermissionDateApprovedYear { get; set; }

    public DateTime? PlanningPermissionDateSubmitted { 
        get {
            return PlanningPermissionDateSubmittedYear.HasValue && PlanningPermissionDateSubmittedMonth.HasValue ?
                    new DateTime(PlanningPermissionDateSubmittedYear.Value, PlanningPermissionDateSubmittedMonth.Value, 1) : null;
        } 
    }

    public DateTime? PlanningPermissionDateApproved
    {
        get
        {
            return PlanningPermissionDateApprovedYear.HasValue && PlanningPermissionDateApprovedMonth.HasValue ?
                new DateTime(PlanningPermissionDateApprovedYear.Value, PlanningPermissionDateApprovedMonth.Value, 1) : null;
        }
    }
}

public class SetTellUsAboutPlanningPermissionResponse
{
    public bool HasChangedDates { get; set; }
}
