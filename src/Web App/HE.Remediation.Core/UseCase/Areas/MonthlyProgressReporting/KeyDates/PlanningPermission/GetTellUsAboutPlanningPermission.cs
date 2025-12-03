using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectSupport;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;
public class GetTellUsAboutPlanningPermission : IRequestHandler<GetTellUsAboutPlanningPermissionRequest, GetTellUsAboutPlanningPermissionResponse>
{
    private readonly IApplicationDataProvider _dataProvider;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public GetTellUsAboutPlanningPermission(IApplicationDataProvider dataProvider,
        IApplicationDetailsProvider detailsProvider,
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _dataProvider = dataProvider;
        _detailsProvider = detailsProvider;
        _keyDatesRepository = keyDatesRepository;
    }
    public async Task<GetTellUsAboutPlanningPermissionResponse> Handle(GetTellUsAboutPlanningPermissionRequest request, CancellationToken cancellationToken)
    {
        var progressReportId = _dataProvider.GetProgressReportId();
        var applicationDetails = await _detailsProvider.GetApplicationDetails();
        var tellUsAboutYourPlanningPermission = await _keyDatesRepository.GetProgressReportTellUsAboutPlanningPermission(new GetProgressReportTellUsAboutPlanningPermissionParameters
        {
            ApplicationId = applicationDetails.ApplicationId,
            ProgressReportId = progressReportId
        });
        return new GetTellUsAboutPlanningPermissionResponse
        {
            ApplicationReferenceNumber = applicationDetails.ApplicationReferenceNumber,
            BuildingName = applicationDetails.BuildingName,
            PlanningPermissionDateSubmittedMonth = tellUsAboutYourPlanningPermission?.PlanningPermissionDateSubmitted.HasValue == true ? tellUsAboutYourPlanningPermission.PlanningPermissionDateSubmitted.Value.Month : null,
            PlanningPermissionDateSubmittedYear = tellUsAboutYourPlanningPermission?.PlanningPermissionDateSubmitted.HasValue == true ? tellUsAboutYourPlanningPermission.PlanningPermissionDateSubmitted.Value.Year : null,
            PlanningPermissionDateApprovedMonth = tellUsAboutYourPlanningPermission?.PlanningPermissionDateApproved.HasValue == true ? tellUsAboutYourPlanningPermission.PlanningPermissionDateApproved.Value.Month : null,
            PlanningPermissionDateApprovedYear = tellUsAboutYourPlanningPermission?.PlanningPermissionDateApproved.HasValue == true ? tellUsAboutYourPlanningPermission.PlanningPermissionDateApproved.Value.Year : null,
            PreviousPlanningPermissionDateSubmitted = tellUsAboutYourPlanningPermission?.PreviousPlanningPermissionDateSubmitted,
            PreviousPlanningPermissionDateApproved = tellUsAboutYourPlanningPermission?.PreviousPlanningPermissionDateApproved
        };
    }
}

public class GetTellUsAboutPlanningPermissionRequest : IRequest<GetTellUsAboutPlanningPermissionResponse>
{
    private GetTellUsAboutPlanningPermissionRequest()
    {
    }
    public static readonly GetTellUsAboutPlanningPermissionRequest Request = new GetTellUsAboutPlanningPermissionRequest();
}

public class GetTellUsAboutPlanningPermissionResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public int? PlanningPermissionDateSubmittedMonth { get; set; }
    public int? PlanningPermissionDateSubmittedYear { get; set; }
    public int? PlanningPermissionDateApprovedMonth { get; set; }
    public int? PlanningPermissionDateApprovedYear { get; set; }
    public DateTime? PreviousPlanningPermissionDateSubmitted { get; set; }
    public DateTime? PreviousPlanningPermissionDateApproved { get; set; }
}
