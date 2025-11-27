using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;
public class GetPlanningPermission : IRequestHandler<GetPlanningPermissionRequest, GetPlanningPermissionResponse>
{
    private readonly IApplicationDataProvider _dataProvider;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;
    public GetPlanningPermission(IApplicationDataProvider dataProvider,
        IApplicationDetailsProvider detailsProvider,
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _dataProvider = dataProvider;
        _detailsProvider = detailsProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async Task<GetPlanningPermissionResponse> Handle(GetPlanningPermissionRequest request, CancellationToken cancellationToken)
    {
        var progressReportId = _dataProvider.GetProgressReportId();
        var applicationDetails = await _detailsProvider.GetApplicationDetails();
        var planningPermission = await _keyDatesRepository.GetProgressReportPlanningPermissionKeyDates(new GetProgressReportPlanningPermissionKeyDatesParameters
        {
            ApplicationId = applicationDetails.ApplicationId,
            ProgressReportId = progressReportId
        });
        return new GetPlanningPermissionResponse
        {
            ApplicationReferenceNumber = applicationDetails.ApplicationReferenceNumber,
            BuildingName = applicationDetails.BuildingName,
            WorksNeedPlanningPermission = planningPermission?.WorksNeedPlanningPermission
        };
    }
}

public class GetPlanningPermissionRequest : IRequest<GetPlanningPermissionResponse>
{
    private GetPlanningPermissionRequest()
    {
    }
    public static readonly GetPlanningPermissionRequest Request = new GetPlanningPermissionRequest();
}

public class GetPlanningPermissionResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public EYesNoNonBoolean? WorksNeedPlanningPermission { get; set; }
}