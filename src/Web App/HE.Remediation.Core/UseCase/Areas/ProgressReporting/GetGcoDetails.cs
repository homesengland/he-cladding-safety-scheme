using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetGcoDetailsHandler : IRequestHandler<GetGcoDetailsRequest, GetGcoDetailsResponse>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetGcoDetailsHandler(IApplicationRepository applicationRepository, IBuildingDetailsRepository buildingDetailsRepository, IProgressReportingRepository progressReportingRepository, IApplicationDataProvider applicationDataProvider)
    {
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _progressReportingRepository = progressReportingRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetGcoDetailsResponse> Handle(GetGcoDetailsRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var details = await _progressReportingRepository.GetGcoDetails();

        var gcoComplete = await _progressReportingRepository.IsGrantCertifyingOfficerComplete();
        var version = await _progressReportingRepository.GetProgressReportVersion();

        return new GetGcoDetailsResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            TeamMemberId = details.Id,
            CompanyName = details.CompanyName,
            Name = details.Name,
            Role = details.Role,
            RoleId = details.RoleId,
            CompanyRegistrationNumber = details.CompanyRegistrationNumber,
            EmailAddress = details.EmailAddress,
            PhoneNumber = details.PrimaryContactNumber,
            IsContractSigned = details.ContractSigned,
            HasIndemnityInsurance = details.IndemnityInsurance,
            IsInvolvedInOriginalInstallation = details.InvolvedInOriginalInstallation,
            CertifyingOfficerResponse = (ECertifyingOfficerResponse?)details.CertifyingOfficerResponseId,
            IsGcoComplete = gcoComplete,
            Version = version
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

    public string CompanyRegistrationNumber { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public bool? IsContractSigned { get; set; }
    public bool? HasIndemnityInsurance { get; set; }
    public bool? IsInvolvedInOriginalInstallation { get; set; }
    public ECertifyingOfficerResponse? CertifyingOfficerResponse { get; set; }

    public int Version { get; set; }
    public bool IsGcoComplete { get; set; }
}