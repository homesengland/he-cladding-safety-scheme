using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.GetTeamMember;

public class GetTeamMemberHandler : IRequestHandler<GetTeamMemberRequest, GetTeamMemberResponse>
{
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetTeamMemberHandler(
        IBuildingDetailsRepository buildingDetailsRepository, 
        IApplicationRepository applicationRepository, 
        IApplicationDataProvider applicationDataProvider, IProgressReportingRepository progressReportingRepository)
    {
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _applicationDataProvider = applicationDataProvider;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetTeamMemberResponse> Handle(GetTeamMemberRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var referenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var version = await _progressReportingRepository.GetProgressReportVersion();

        var teamMember = await _progressReportingRepository.GetTeamMember(request.TeamMemberId ?? request.Selected);

        if (teamMember is null && (request.TeamMemberId.HasValue || request.Selected.HasValue))
        {
            throw new EntityNotFoundException("Team Member not found");
        }

        return new GetTeamMemberResponse
        {
            Role = request.TeamMemberId.HasValue ? (teamMember?.Role ?? request.TeamRole) : request.TeamRole,
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
            TeamMemberId = request.TeamMemberId.HasValue ? teamMember?.TeamMemberId : null,
            InvolvedInOriginalInstallation = teamMember?.InvolvedInOriginalInstallation,
            PrimaryContactNumber = teamMember?.PrimaryContactNumber,
            InvolvedRoleReason = teamMember?.InvolvedRoleReason,
            Name = teamMember?.Name,
            Version = version,
            HasChasCertification = teamMember?.HasChasCertification
        };
    }
}