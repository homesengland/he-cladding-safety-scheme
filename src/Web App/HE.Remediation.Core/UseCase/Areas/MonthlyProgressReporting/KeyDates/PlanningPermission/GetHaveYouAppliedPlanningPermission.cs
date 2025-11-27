using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;
public class GetHaveYouAppliedPlanningPermission : IRequestHandler<GetHaveYouAppliedPlanningPermissionRequest, GetHaveYouAppliedPlanningPermissionResponse>
{
    private readonly IApplicationDataProvider _dataProvider;
    private readonly IApplicationDetailsProvider _detailsProvider;  
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;
    public GetHaveYouAppliedPlanningPermission(IApplicationDataProvider dataProvider,
        IApplicationDetailsProvider detailsProvider,
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _dataProvider = dataProvider;
        _detailsProvider = detailsProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async Task<GetHaveYouAppliedPlanningPermissionResponse> Handle(GetHaveYouAppliedPlanningPermissionRequest request, CancellationToken cancellationToken)
    {
        var progressReportId = _dataProvider.GetProgressReportId();
        var applicationDetails = await _detailsProvider.GetApplicationDetails();
        var haveYouAppliedPlanningPermission = await _keyDatesRepository.GetProgressReportHaveYouAppliedPlanningPermission(new GetProgressReportHaveYouAppliedPlanningPermissionParameters
        {
            ApplicationId = applicationDetails.ApplicationId,
            ProgressReportId = progressReportId
        });
        return new GetHaveYouAppliedPlanningPermissionResponse
        {
            ApplicationReferenceNumber = applicationDetails.ApplicationReferenceNumber,
            BuildingName = applicationDetails.BuildingName,
            HaveAppliedPlanningPermission = haveYouAppliedPlanningPermission
        };
    }
}

public class GetHaveYouAppliedPlanningPermissionRequest : IRequest<GetHaveYouAppliedPlanningPermissionResponse>
{
    private GetHaveYouAppliedPlanningPermissionRequest()
    {
    }
    public static readonly GetHaveYouAppliedPlanningPermissionRequest Request = new GetHaveYouAppliedPlanningPermissionRequest();
}

public class GetHaveYouAppliedPlanningPermissionResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool? HaveAppliedPlanningPermission { get; set; }
}