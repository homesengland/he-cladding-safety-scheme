using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.WorksPlanning;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.WorksPlanning;

public class GetWorksPlanningDatesChangedHandler : IRequestHandler<GetWorksPlanningDatesChangedRequest, GetWorksPlanningDatesChangedResponse>
{
    private readonly IApplicationDataProvider _dataProvider;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public GetWorksPlanningDatesChangedHandler(
        IApplicationDataProvider dataProvider, 
        IApplicationDetailsProvider detailsProvider, 
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _dataProvider = dataProvider;
        _detailsProvider = detailsProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async Task<GetWorksPlanningDatesChangedResponse> Handle(GetWorksPlanningDatesChangedRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _detailsProvider.GetApplicationDetails();
        var reportId = _dataProvider.GetProgressReportId();

        var datesChanged = await _keyDatesRepository.GetWorksPlanningDatesChanged(
            new GetWorksPlanningDatesChangedParameters
            {
                ApplicationId = details.ApplicationId,
                ProgressReportId = reportId
            });

        var changeTypes = await _keyDatesRepository.GetProgressReportKeyDatesChangeTypes();

        return new GetWorksPlanningDatesChangedResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            ChangeTypeId = datesChanged?.WorksPlanningChangeTypeId,
            ChangeReason = datesChanged?.WorksPlanningChangeReason,
            ChangeTypes = changeTypes.ToList()
        };
    }
}

public class GetWorksPlanningDatesChangedRequest : IRequest<GetWorksPlanningDatesChangedResponse>
{
    private GetWorksPlanningDatesChangedRequest()
    {
    }

    public static readonly GetWorksPlanningDatesChangedRequest Request = new();
}

public class GetWorksPlanningDatesChangedResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public int? ChangeTypeId { get; set; }
    public string ChangeReason { get; set; }

    public IList<GetProgressReportKeyDatesChangeTypesResult> ChangeTypes { get; set; } = new List<GetProgressReportKeyDatesChangeTypesResult>();
}