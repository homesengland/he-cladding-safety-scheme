using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.TeamMember.Get;

public class GetTeamMemberHandler : IRequestHandler<GetTeamMemberRequest, GetTeamMemberResponse>
{
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetTeamMemberHandler(
        IBuildingDetailsRepository buildingDetailsRepository,
        IApplicationRepository applicationRepository,
        IApplicationDataProvider applicationDataProvider,
        IWorkPackageRepository workPackageRepository)
    {
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _applicationDataProvider = applicationDataProvider;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetTeamMemberResponse> Handle(GetTeamMemberRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var referenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var teamMember = request.TeamMemberId.HasValue
            ? await _workPackageRepository.GetTeamMember(request.TeamMemberId)
            : request.Selected.HasValue
                ? await _workPackageRepository.GetTeamMember(request.Selected)
                : null;

        if (teamMember is null && request.TeamMemberId.HasValue)
        {
            throw new EntityNotFoundException("Team Member not found");
        }

        return new GetTeamMemberResponse
        {
            TeamMemberId = request.TeamMemberId.HasValue ? teamMember?.TeamMemberId : null,
            Role = (request.TeamMemberId.HasValue ? teamMember?.Role : request.TeamRole) ?? request.TeamRole,
            OtherRole = teamMember?.OtherRole,
            ApplicationReferenceNumber = referenceNumber,
            BuildingName = buildingName,
            CompanyName = teamMember?.CompanyName,
            CompanyRegistration = teamMember?.CompanyRegistration,
            ContractSigned = teamMember?.ContractSigned,
            EmailAddress = teamMember?.EmailAddress,
            IndemnityInsurance = teamMember?.IndemnityInsurance,
            IndemnityInsuranceReason = teamMember?.IndemnityInsuranceReason,
            InvolvedInOriginalInstallation = teamMember?.InvolvedInOriginalInstallation,
            PrimaryContactNumber = teamMember?.PrimaryContactNumber,
            InvolvedRoleReason = teamMember?.InvolvedRoleReason,
            Name = teamMember?.Name,
            ConsiderateConstructorSchemeType = teamMember?.ConsiderateConstructorSchemeType,
            IsCopy = request.Selected.HasValue,
            HasChasCertification = teamMember?.HasChasCertification,
            ConsiderateConstructorSchemeReasonNo = teamMember?.ConsiderateConstructorSchemeType == EConsiderateConstructorSchemeType.No ? teamMember.ConsiderateConstructorSchemeReason : null,
            ConsiderateConstructorSchemeReasonDontKnow = teamMember?.ConsiderateConstructorSchemeType == EConsiderateConstructorSchemeType.DontKnow ? teamMember.ConsiderateConstructorSchemeReason : null
        };
    }
}