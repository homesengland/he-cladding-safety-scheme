using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.PlanningPermission;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;

public class GetPlanningPermissionDatesChangedHandler : IRequestHandler<GetPlanningPermissionDatesChangedRequest, GetPlanningPermissionDatesChangedResponse>
{
    private readonly IApplicationDataProvider _dataProvider;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public GetPlanningPermissionDatesChangedHandler(
        IApplicationDataProvider dataProvider, 
        IApplicationDetailsProvider detailsProvider, 
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _dataProvider = dataProvider;
        _detailsProvider = detailsProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async Task<GetPlanningPermissionDatesChangedResponse> Handle(GetPlanningPermissionDatesChangedRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _detailsProvider.GetApplicationDetails();
        var reportId = _dataProvider.GetProgressReportId();

        var datesChanged = await _keyDatesRepository.GetPlanningPermissionDatesChanged(
            new GetPlanningPermissionDatesChangedParameters
            {
                ApplicationId = details.ApplicationId,
                ProgressReportId = reportId
            });

        var changeTypes = await _keyDatesRepository.GetProgressReportKeyDatesChangeTypes();

        var appliedForPlanningPermission = await _keyDatesRepository.GetProgressReportHaveYouAppliedPlanningPermission(
            new GetProgressReportHaveYouAppliedPlanningPermissionParameters
            {
                ApplicationId = details.ApplicationId,
                ProgressReportId = reportId
            });

        return new GetPlanningPermissionDatesChangedResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            ChangeTypeId = datesChanged?.PlanningPermissionChangeTypeId,
            ChangeReason = datesChanged?.PlanningPermissionChangeReason,
            ChangeTypes = changeTypes.ToList(),
            AppliedForPlanningPermission = appliedForPlanningPermission
        };
    }
}

public class GetPlanningPermissionDatesChangedRequest : IRequest<GetPlanningPermissionDatesChangedResponse>
{
    private GetPlanningPermissionDatesChangedRequest()
    {
    }

    public static readonly GetPlanningPermissionDatesChangedRequest Request = new();
}

public class GetPlanningPermissionDatesChangedResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public int? ChangeTypeId { get; set; }
    public string ChangeReason { get; set; }

    public IList<GetProgressReportKeyDatesChangeTypesResult> ChangeTypes { get; set; } = new List<GetProgressReportKeyDatesChangeTypesResult>();

    public bool? AppliedForPlanningPermission { get; set; }
}