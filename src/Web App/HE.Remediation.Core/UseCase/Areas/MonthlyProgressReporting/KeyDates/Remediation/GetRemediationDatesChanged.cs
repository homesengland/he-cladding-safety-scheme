using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.Remediation;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.Remediation;

public class GetRemediationDatesChangedHandler : IRequestHandler<GetRemediationDatesChangedRequest, GetRemediationDatesChangedResponse>
{
    private readonly IApplicationDataProvider _dataProvider;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public GetRemediationDatesChangedHandler(
        IApplicationDataProvider dataProvider, 
        IApplicationDetailsProvider detailsProvider, 
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _dataProvider = dataProvider;
        _detailsProvider = detailsProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async ValueTask<GetRemediationDatesChangedResponse> Handle(GetRemediationDatesChangedRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _detailsProvider.GetApplicationDetails();
        var reportId = _dataProvider.GetProgressReportId();

        var datesChanged = await _keyDatesRepository.GetRemediationDatesChanged(
            new GetRemediationDatesChangedParameters
            {
                ApplicationId = details.ApplicationId,
                ProgressReportId = reportId
            });

        var changeTypes = await _keyDatesRepository.GetProgressReportKeyDatesChangeTypes();

        return new GetRemediationDatesChangedResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            ChangeTypeId = datesChanged?.RemediationChangeTypeId,
            ChangeReason = datesChanged?.RemediationChangeReason,
            ChangeTypes = changeTypes.ToList()
        };
    }
}

public class GetRemediationDatesChangedRequest : IRequest<GetRemediationDatesChangedResponse>
{
    private GetRemediationDatesChangedRequest()
    {
    }

    public static readonly GetRemediationDatesChangedRequest Request = new();
}

public class GetRemediationDatesChangedResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public int? ChangeTypeId { get; set; }
    public string ChangeReason { get; set; }

    public IList<GetProgressReportKeyDatesChangeTypesResult> ChangeTypes { get; set; } = new List<GetProgressReportKeyDatesChangeTypesResult>();
}