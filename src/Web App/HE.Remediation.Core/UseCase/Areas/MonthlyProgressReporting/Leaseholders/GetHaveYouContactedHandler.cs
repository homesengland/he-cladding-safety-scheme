using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders;

public class GetHaveYouContactedHandler : IRequestHandler<GetHaveYouContactedRequest, GetHaveYouContactedResponse>
{
    private readonly IProgressReportingLeaseholdersRepository _progressReportingLeaseholdersRepository;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IApplicationDataProvider _dataProvider;

    public GetHaveYouContactedHandler(
        IApplicationDetailsProvider detailsProvider,
        IApplicationDataProvider dataProvider,
        IProgressReportingLeaseholdersRepository progressReportingLeaseholdersRepository
    )
    {
        _detailsProvider = detailsProvider;
        _dataProvider = dataProvider;
        _progressReportingLeaseholdersRepository = progressReportingLeaseholdersRepository;
    }

    public async Task<GetHaveYouContactedResponse> Handle(GetHaveYouContactedRequest request, CancellationToken cancellationToken)
    {
        var details = await _detailsProvider.GetApplicationDetails();
        var parameters = new GetProgressReportLeaseholderCommunicationParameters()
        {
            ApplicationId = details.ApplicationId,
            ProgressReportId = _dataProvider.GetProgressReportId()
        };
        var result = await _progressReportingLeaseholdersRepository.GetProgressReportLeaseholderCommunication(parameters);
        return new GetHaveYouContactedResponse()
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            HasContacted = result?.HasContacted,
            LastCommunicationDate = result?.LastCommunicationDate,
            PreviousLastCommunicationDate = result?.PreviousLastCommunicationDate
        };
    }
}

public class GetHaveYouContactedRequest() : IRequest<GetHaveYouContactedResponse> {}

public class GetHaveYouContactedResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public ENoYes? HasContacted { get; set; }

    public DateTime? LastCommunicationDate { get; set; }
    public DateTime? PreviousLastCommunicationDate { get; set; }
}
