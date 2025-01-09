using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetGrantCertifyingOfficerCheckYourAnswersHandler : IRequestHandler<GetGrantCertifyingOfficerCheckYourAnswersRequest, GetGrantCertifyingOfficerCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetGrantCertifyingOfficerCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider, IApplicationRepository applicationRepository, IBuildingDetailsRepository buildingDetailsRepository, IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetGrantCertifyingOfficerCheckYourAnswersResponse> Handle(GetGrantCertifyingOfficerCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var version = await _progressReportingRepository.GetProgressReportVersion();

        var teamMember = await _progressReportingRepository.GetTeamMember(request.TeamMemberId);
        var isGcoComplete = await _progressReportingRepository.IsGrantCertifyingOfficerComplete();

        return new GetGrantCertifyingOfficerCheckYourAnswersResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            CompanyName = teamMember.CompanyName,
            CompanyRegistration = teamMember.CompanyRegistration,
            Role = teamMember.Role,
            OtherRole = teamMember.OtherRole,
            IndemnityInsuranceReason = teamMember.IndemnityInsuranceReason,
            EmailAddress = teamMember.EmailAddress,
            IndemnityInsurance = teamMember.IndemnityInsurance,
            PrimaryContactNumber = teamMember.PrimaryContactNumber,
            InvolvedInOriginalInstallation = teamMember.InvolvedInOriginalInstallation,
            ContractSigned = teamMember.ContractSigned,
            ConsiderateConstructorSchemeType = teamMember.ConsiderateConstructorSchemeType,
            InvolvedRoleReason = teamMember.InvolvedRoleReason,
            Name = teamMember.Name,
            TeamMemberId = teamMember.TeamMemberId,
            Version = version,
            IsGcoComplete = isGcoComplete
        };
    }
}

public class GetGrantCertifyingOfficerCheckYourAnswersRequest : IRequest<GetGrantCertifyingOfficerCheckYourAnswersResponse>
{
    public Guid TeamMemberId { get; set; }
}

public class GetGrantCertifyingOfficerCheckYourAnswersResponse
{
    public Guid? TeamMemberId { get; set; }
    public ETeamRole? Role { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistration { get; set; }
    public string EmailAddress { get; set; }
    public string PrimaryContactNumber { get; set; }
    public string OtherRole { get; set; }
    public bool? ContractSigned { get; set; }
    public bool? IndemnityInsurance { get; set; }
    public bool? InvolvedInOriginalInstallation { get; set; }
    public string IndemnityInsuranceReason { get; set; }
    public string InvolvedRoleReason { get; set; }
    public EConsiderateConstructorSchemeType? ConsiderateConstructorSchemeType { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public int Version { get; set; }
    public bool IsGcoComplete { get; set; }
}