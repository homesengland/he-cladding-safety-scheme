using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class GetGcoDetailsHandler : IRequestHandler<GetGcoDetailsRequest, GetGcoDetailsResponse>
{
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public GetGcoDetailsHandler(
        IApplicationDetailsProvider detailsProvider,
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _detailsProvider = detailsProvider;
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async ValueTask<GetGcoDetailsResponse> Handle(GetGcoDetailsRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _detailsProvider.GetApplicationDetails();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var gcoDetails = await _progressReportingProjectTeamRepository.GetGrantCertifyingOfficer(progressReportId);

        return new GetGcoDetailsResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            TeamMemberId = gcoDetails.TeamMemberId.Value,
            CompanyName = gcoDetails.CompanyName,
            Name = gcoDetails.TeamMemberName,
            Role = gcoDetails.TeamMemberRole,
            RoleId = gcoDetails.TeamMemberRoleId.Value,
            CompanyRegistration = gcoDetails.CompanyRegistrationNumber,
            EmailAddress = gcoDetails.EmailAddress,
            PrimaryContactNumber = gcoDetails.PrimaryContactNumber,
            IsContractSigned = gcoDetails.ContractStartDate != null,
            HasIndemnityInsurance = gcoDetails.IndemnityInsurance,
            IsInvolvedInOriginalInstallation = gcoDetails.InvolvedInOriginalInstallation,
            IndemnityInsuranceReason = gcoDetails.IndemnityInsuranceReason,
            InvolvedRoleReason = gcoDetails.InvolvedRoleReason,
            CertifyingOfficerResponse = gcoDetails.CertifyingOfficerResponse
        };
    }
}

public class GetGcoDetailsRequest : IRequest<GetGcoDetailsResponse>
{
    private GetGcoDetailsRequest()
    {
    }

    public static readonly GetGcoDetailsRequest Request = new();
}

public class GetGcoDetailsResponse
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }

    public Guid TeamMemberId { get; set; }
    public string CompanyName { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public int RoleId { get; set; }

    public string CompanyRegistration { get; set; }
    public string EmailAddress { get; set; }
    public string PrimaryContactNumber { get; set; }
    public bool? IsContractSigned { get; set; }
    public bool? HasIndemnityInsurance { get; set; }
    public bool? IsInvolvedInOriginalInstallation { get; set; }
    public string IndemnityInsuranceReason { get; set; }
    public string InvolvedRoleReason { get; set; }
    public ECertifyingOfficerResponse? CertifyingOfficerResponse { get; set; }
}