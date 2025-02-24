using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetGrantCertifyingOfficerDetailsHandler : IRequestHandler<GetGrantCertifyingOfficerDetailsRequest, GetGrantCertifyingOfficerDetailsResponse>
{
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetGrantCertifyingOfficerDetailsHandler(IBuildingDetailsRepository buildingDetailsRepository, IApplicationRepository applicationRepository, IApplicationDataProvider applicationDataProvider, IProgressReportingRepository progressReportingRepository)
    {
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _applicationDataProvider = applicationDataProvider;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetGrantCertifyingOfficerDetailsResponse> Handle(GetGrantCertifyingOfficerDetailsRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var referenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var version = await _progressReportingRepository.GetProgressReportVersion();

        var teamMember = await _progressReportingRepository.GetTeamMember(request.TeamMemberId);

        if (teamMember is null && request.TeamMemberId.HasValue)
        {
            throw new EntityNotFoundException("Team Member not found");
        }

        return new GetGrantCertifyingOfficerDetailsResponse
        {
            Role = teamMember?.Role ?? request.TeamRole,
            OtherRole = teamMember?.OtherRole,
            ApplicationReferenceNumber = referenceNumber,
            BuildingName = buildingName,
            CompanyName = teamMember?.CompanyName,
            CompanyRegistration = teamMember?.CompanyRegistration,
            ConsiderateConstructorSchemeType = teamMember?.ConsiderateConstructorSchemeType,
            ContractSigned = teamMember?.ContractSigned,
            EmailAddress = teamMember?.EmailAddress,
            IndemnityInsurance = teamMember?.IndemnityInsurance,
            IndemnityInsuranceReason = teamMember?.IndemnityInsuranceReason,
            TeamMemberId = teamMember?.TeamMemberId,
            InvolvedInOriginalInstallation = teamMember?.InvolvedInOriginalInstallation,
            PrimaryContactNumber = teamMember?.PrimaryContactNumber,
            InvolvedRoleReason = teamMember?.InvolvedRoleReason,
            Name = teamMember?.Name
        };
    }
}

public class GetGrantCertifyingOfficerDetailsRequest : IRequest<GetGrantCertifyingOfficerDetailsResponse>
{
    public Guid? TeamMemberId { get; set; }
    public ETeamRole TeamRole { get; set; }
}

public class GetGrantCertifyingOfficerDetailsResponse
{
    public Guid? TeamMemberId { get; set; }
    public ETeamRole Role { get; set; }
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

    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
}