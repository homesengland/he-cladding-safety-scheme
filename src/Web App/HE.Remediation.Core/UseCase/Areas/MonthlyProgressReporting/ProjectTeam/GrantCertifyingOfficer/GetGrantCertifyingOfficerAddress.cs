using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class GetGrantCertifyingOfficerAddressHandler : IRequestHandler<GetGrantCertifyingOfficerAddressRequest, GetGrantCertifyingOfficerAddressResponse>
{
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public GetGrantCertifyingOfficerAddressHandler(
        IApplicationDetailsProvider detailsProvider,
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _detailsProvider = detailsProvider;
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async ValueTask<GetGrantCertifyingOfficerAddressResponse> Handle(GetGrantCertifyingOfficerAddressRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _detailsProvider.GetApplicationDetails();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var address = await _progressReportingProjectTeamRepository.GetGrantCertifyingOfficer(progressReportId);

        return new GetGrantCertifyingOfficerAddressResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,

            NameNumber = address?.NameNumber,
            AddressLine1 = address?.AddressLine1,
            AddressLine2 = address?.AddressLine2,
            City = address?.City,
            County = address?.County,
            PostCode = address?.Postcode
        };
    }
}

public class GetGrantCertifyingOfficerAddressRequest : IRequest<GetGrantCertifyingOfficerAddressResponse>
{
    private GetGrantCertifyingOfficerAddressRequest()
    {
    }

    public static readonly GetGrantCertifyingOfficerAddressRequest Request = new();
}

public class GetGrantCertifyingOfficerAddressResponse
{
    public string NameNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string PostCode { get; set; }
    public string ReturnUrl { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public int ProgressReportVersion { get; set; }
    public bool IsProgressReportGcoComplete { get; set; }
    public bool ProgressReportHasVisitedCheckYourAnswers { get; set; }
}