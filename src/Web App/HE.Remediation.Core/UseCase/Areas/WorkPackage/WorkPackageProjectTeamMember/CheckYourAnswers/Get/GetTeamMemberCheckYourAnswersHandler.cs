using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.CheckYourAnswers.Get;

public class GetTeamMemberCheckYourAnswersHandler : IRequestHandler<GetTeamMemberCheckYourAnswersRequest, GetTeamMemberCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetTeamMemberCheckYourAnswersHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetTeamMemberCheckYourAnswersResponse> Handle(GetTeamMemberCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var teamMember = await _workPackageRepository.GetTeamMember(request.TeamMemberId);

        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();
        
        return new GetTeamMemberCheckYourAnswersResponse
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
            IsSubmitted = isSubmitted,
            HasChasCertification = teamMember.HasChasCertification
        };
    }
}