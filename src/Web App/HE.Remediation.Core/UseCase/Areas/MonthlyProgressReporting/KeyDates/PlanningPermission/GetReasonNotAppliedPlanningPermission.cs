using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;
public class GetReasonNotAppliedPlanningPermission : IRequestHandler<GetReasonNotAppliedPlanningPermissionRequest, GetReasonNotAppliedPlanningPermissionResponse>
{
    private readonly IApplicationDataProvider _dataProvider;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;
    public GetReasonNotAppliedPlanningPermission(IApplicationDataProvider dataProvider,
        IApplicationDetailsProvider detailsProvider,
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _dataProvider = dataProvider;
        _detailsProvider = detailsProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async ValueTask<GetReasonNotAppliedPlanningPermissionResponse> Handle(GetReasonNotAppliedPlanningPermissionRequest request, CancellationToken cancellationToken)
    {
        var progressReportId = _dataProvider.GetProgressReportId();
        var applicationDetails = await _detailsProvider.GetApplicationDetails();

        var reasonNotAppliedPlanningPermission = await _keyDatesRepository.GetProgressReportReasonNotAppliedPlanningPermission(new GetProgressReportReasonNotAppliedPlanningPermissionParameters
        {
            ApplicationId = applicationDetails.ApplicationId,
            ProgressReportId = progressReportId
        });

        return new GetReasonNotAppliedPlanningPermissionResponse
        {
            ApplicationReferenceNumber = applicationDetails.ApplicationReferenceNumber,
            BuildingName = applicationDetails.BuildingName,
            PlannedYearToSubmitApplication = reasonNotAppliedPlanningPermission?.PlanningPermissionPlanToSubmitDate?.Year,
            PlannedMonthToSubmitApplication = reasonNotAppliedPlanningPermission?.PlanningPermissionPlanToSubmitDate?.Month,
            ReasonNotAppliedPlanningPermission = reasonNotAppliedPlanningPermission?.PlanningPermissionReasonNotApplied,
            PreviousPlanToSubmitDate = reasonNotAppliedPlanningPermission?.PreviousPlanningPermissionPlanToSubmitDate
        };
    }
}

public class GetReasonNotAppliedPlanningPermissionRequest : IRequest<GetReasonNotAppliedPlanningPermissionResponse>
{
    private GetReasonNotAppliedPlanningPermissionRequest()
    {
    }
    public static readonly GetReasonNotAppliedPlanningPermissionRequest Request = new GetReasonNotAppliedPlanningPermissionRequest();
}

public class GetReasonNotAppliedPlanningPermissionResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public int? PlannedYearToSubmitApplication { get; set; }
    public int? PlannedMonthToSubmitApplication { get; set; }
    public string ReasonNotAppliedPlanningPermission { get; set; }
    public DateTime? PreviousPlanToSubmitDate { get; set; }
}
