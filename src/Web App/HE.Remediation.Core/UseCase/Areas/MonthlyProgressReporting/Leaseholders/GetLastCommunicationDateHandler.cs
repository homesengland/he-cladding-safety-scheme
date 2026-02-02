using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders;

public class GetLastCommunicationDateHandler : IRequestHandler<GetLastCommunicationDateRequest, GetLastCommunicationDateResponse>
{
    private readonly IProgressReportingLeaseholdersRepository _progressReportingLeaseholdersRepository;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IApplicationDataProvider _dataProvider;

    public GetLastCommunicationDateHandler(
        IApplicationDetailsProvider detailsProvider,
        IApplicationDataProvider dataProvider,
        IProgressReportingLeaseholdersRepository progressReportingLeaseholdersRepository
    )
    {
        _detailsProvider = detailsProvider;
        _dataProvider = dataProvider;
        _progressReportingLeaseholdersRepository = progressReportingLeaseholdersRepository;
    }

    public async ValueTask<GetLastCommunicationDateResponse> Handle(GetLastCommunicationDateRequest request, CancellationToken cancellationToken)
    {
        var details = await _detailsProvider.GetApplicationDetails();
        var parameters = new GetProgressReportLeaseholderCommunicationParameters()
        {
            ApplicationId = details.ApplicationId,
            ProgressReportId = _dataProvider.GetProgressReportId()
        };
        var result = await _progressReportingLeaseholdersRepository.GetProgressReportLeaseholderCommunication(parameters);
        return new GetLastCommunicationDateResponse()
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            LastCommunicationDate = result?.LastCommunicationDate,
            PreviousLastCommunicationDate = result?.PreviousLastCommunicationDate
        };
    }
}

public class GetLastCommunicationDateRequest() : IRequest<GetLastCommunicationDateResponse> {}

public class GetLastCommunicationDateResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public DateTime? LastCommunicationDate { get; set; }
    public DateTime? PreviousLastCommunicationDate { get; set; }
}
