using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting;

public class GetProgressReportsHandler : IRequestHandler<GetProgressReportsRequest, GetProgressReportsResponse>
{
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IMonthlyProgressReportingRepository _monthlyProgressReportingRepository;

    public GetProgressReportsHandler(IApplicationDetailsProvider applicationDetailsProvider,
                                     IMonthlyProgressReportingRepository monthlyProgressReportingRepository)
    {
        _applicationDetailsProvider = applicationDetailsProvider;
        _monthlyProgressReportingRepository = monthlyProgressReportingRepository;
    }

    public async ValueTask<GetProgressReportsResponse> Handle(GetProgressReportsRequest request, CancellationToken cancellationToken)
    {
        var details = await _applicationDetailsProvider.GetApplicationDetails();
        var progressReports = await _monthlyProgressReportingRepository.GetProgressReports(details.ApplicationId);

        return new GetProgressReportsResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            ProgressReports = progressReports
        };
    }
}

public class GetProgressReportsRequest : IRequest<GetProgressReportsResponse>
{
    private GetProgressReportsRequest()
    {
    }
    public static readonly GetProgressReportsRequest Request = new();
}

public class GetProgressReportsResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public IReadOnlyCollection<ProgressReportResult> ProgressReports { get; set; }
}
