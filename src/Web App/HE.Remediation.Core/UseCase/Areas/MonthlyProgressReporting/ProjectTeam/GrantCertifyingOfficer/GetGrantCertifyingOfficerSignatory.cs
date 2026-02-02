using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class GetGrantCertifyingOfficerSignatoryHandler : IRequestHandler<GetGrantCertifyingOfficerSignatoryRequest, GetGrantCertifyingOfficerSignatoryResponse>
{
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public GetGrantCertifyingOfficerSignatoryHandler(
        IApplicationDetailsProvider detailsProvider,
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _detailsProvider = detailsProvider;
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async ValueTask<GetGrantCertifyingOfficerSignatoryResponse> Handle(GetGrantCertifyingOfficerSignatoryRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _detailsProvider.GetApplicationDetails();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var gcoDetails = await _progressReportingProjectTeamRepository.GetGrantCertifyingOfficer(progressReportId);

        return new GetGrantCertifyingOfficerSignatoryResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            Signatory = gcoDetails.AuthorisedSignatory,
            EmailAddress = gcoDetails.AuthorisedSignatoryEmailAddress,
            DateAppointed = gcoDetails.AuthorisedSignatoryDateAppointed
        };
    }
}

public class GetGrantCertifyingOfficerSignatoryRequest : IRequest<GetGrantCertifyingOfficerSignatoryResponse>
{
    private GetGrantCertifyingOfficerSignatoryRequest()
    {
    }

    public static readonly GetGrantCertifyingOfficerSignatoryRequest Request = new();
}

public class GetGrantCertifyingOfficerSignatoryResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public string Signatory { get; set; }
    public string EmailAddress { get; set; }
    public DateTime? DateAppointed { get; set; }
}