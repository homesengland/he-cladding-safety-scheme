using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders;

public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
{
    private readonly IProgressReportingLeaseholdersRepository _progressReportingLeaseholdersRepository;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IApplicationDataProvider _dataProvider;

    public GetCheckYourAnswersHandler(
        IApplicationDetailsProvider detailsProvider,
        IApplicationDataProvider dataProvider,
        IProgressReportingLeaseholdersRepository progressReportingLeaseholdersRepository
    )
    {
        _detailsProvider = detailsProvider;
        _dataProvider = dataProvider;
        _progressReportingLeaseholdersRepository = progressReportingLeaseholdersRepository;
    }

    public async Task<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var details = await _detailsProvider.GetApplicationDetails();
        var parameters = new GetProgressReportLeaseholderCommunicationParameters()
        {
            ApplicationId = details.ApplicationId,
            ProgressReportId = _dataProvider.GetProgressReportId()
        };
        var result = await _progressReportingLeaseholdersRepository.GetProgressReportLeaseholderCommunication(parameters);
        var files = await _progressReportingLeaseholdersRepository.GetProgressReportLeaseholderCommunicationFiles(parameters);

        return new GetCheckYourAnswersResponse()
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            HasContacted = result?.HasContacted,
            LastCommunicationDate = result?.LastCommunicationDate,
            AddedFiles = [.. files.Select(f => f.Name)]
        };
    }
}

public class GetCheckYourAnswersRequest() : IRequest<GetCheckYourAnswersResponse> {}

public class GetCheckYourAnswersResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public ENoYes? HasContacted { get; set; }
    public DateTime? LastCommunicationDate { get; set; }

    public List<string> AddedFiles { get; set; }
}
