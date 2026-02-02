using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.BuildingControl;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.BuildingControl;

public class GetBuildingControlDatesChangedHandler : IRequestHandler<GetBuildingControlDatesChangedRequest, GetBuildingControlDatesChangedResponse>
{
    private readonly IApplicationDataProvider _dataProvider;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public GetBuildingControlDatesChangedHandler(
        IApplicationDataProvider dataProvider, 
        IApplicationDetailsProvider detailsProvider, 
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _dataProvider = dataProvider;
        _detailsProvider = detailsProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async ValueTask<GetBuildingControlDatesChangedResponse> Handle(GetBuildingControlDatesChangedRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _detailsProvider.GetApplicationDetails();
        var reportId = _dataProvider.GetProgressReportId();

        var datesChanged = await _keyDatesRepository.GetBuildingControlDatesChanged(
            new GetBuildingControlDatesChangedParameters
            {
                ApplicationId = details.ApplicationId,
                ProgressReportId = reportId
            });

        var changeTypes = await _keyDatesRepository.GetProgressReportKeyDatesChangeTypes();

        return new GetBuildingControlDatesChangedResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            ChangeTypeId = datesChanged?.BuildingControlChangeTypeId,
            ChangeReason = datesChanged?.BuildingControlChangeReason,
            ChangeTypes = changeTypes.ToList()
        };
    }
}

public class GetBuildingControlDatesChangedRequest : IRequest<GetBuildingControlDatesChangedResponse>
{
    private GetBuildingControlDatesChangedRequest()
    {
    }

    public static readonly GetBuildingControlDatesChangedRequest Request = new();
}

public class GetBuildingControlDatesChangedResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public int? ChangeTypeId { get; set; }
    public string ChangeReason { get; set; }

    public IList<GetProgressReportKeyDatesChangeTypesResult> ChangeTypes { get; set; } = new List<GetProgressReportKeyDatesChangeTypesResult>();
}