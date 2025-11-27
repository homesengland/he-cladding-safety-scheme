using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class GetHasGrantCertifyingOfficerHandler : IRequestHandler<GetHasGrantCertifyingOfficerRequest, GetHasGrantCertifyingOfficerResponse>
{
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public GetHasGrantCertifyingOfficerHandler(
        IApplicationDetailsProvider detailsProvider,
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _detailsProvider = detailsProvider;
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async Task<GetHasGrantCertifyingOfficerResponse> Handle(GetHasGrantCertifyingOfficerRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _detailsProvider.GetApplicationDetails();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var gcoDetails = await _progressReportingProjectTeamRepository.GetGrantCertifyingOfficer(progressReportId);

        return new GetHasGrantCertifyingOfficerResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            DoYouHaveAGrantCertifyingOfficer = gcoDetails?.HasGco,
            IsGcoComplete = gcoDetails?.IsGcoComplete ?? false
        };
    }
}

public class GetHasGrantCertifyingOfficerRequest : IRequest<GetHasGrantCertifyingOfficerResponse>
{
    private GetHasGrantCertifyingOfficerRequest()
    {
    }

    public static readonly GetHasGrantCertifyingOfficerRequest Request = new();
}

public class GetHasGrantCertifyingOfficerResponse
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public bool IsGcoComplete { get; set; }
    public bool? DoYouHaveAGrantCertifyingOfficer { get; set; }
}