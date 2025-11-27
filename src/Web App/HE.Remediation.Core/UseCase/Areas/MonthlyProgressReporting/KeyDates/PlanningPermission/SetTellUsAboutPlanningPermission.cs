using System.Transactions;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
using HE.Remediation.Core.Interface;
using MediatR;

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
    public async Task<SetTellUsAboutPlanningPermissionResponse> Handle(SetTellUsAboutPlanningPermissionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var hasChangedDates = await _keyDatesRepository.SetProgressReportTellUsAboutPlanningPermission(new SetProgressReportTellUsAboutPlanningPermissionParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            PlanningPermissionDateSubmitted = request.PlanningPermissionDateSubmittedYear.HasValue && request.PlanningPermissionDateSubmittedMonth.HasValue ?
                new DateTime(request.PlanningPermissionDateSubmittedYear.Value, request.PlanningPermissionDateSubmittedMonth.Value, 1) : null,
            PlanningPermissionDateApproved = request.PlanningPermissionDateApprovedYear.HasValue && request.PlanningPermissionDateApprovedMonth.HasValue ?
                new DateTime(request.PlanningPermissionDateApprovedYear.Value, request.PlanningPermissionDateApprovedMonth.Value, 1) : null
        });

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
}

public class SetTellUsAboutPlanningPermissionResponse
{
    public bool HasChangedDates { get; set; }
}
