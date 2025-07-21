using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.GetTeamMemberCheckYourAnswers;

public class GetTeamMemberCheckYourAnswersHandler : IRequestHandler<GetTeamMemberCheckYourAnswersRequest, GetTeamMemberCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetTeamMemberCheckYourAnswersHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetTeamMemberCheckYourAnswersResponse> Handle(GetTeamMemberCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var version = await _progressReportingRepository.GetProgressReportVersion();

        var teamMember = await _progressReportingRepository.GetTeamMember(request.TeamMemberId);

        var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(
            new GetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = _applicationDataProvider.GetProgressReportId()
            });

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
            HasChasCertification = teamMember.HasChasCertification,
            ConsiderateConstructorSchemeReason = teamMember.ConsiderateConstructorSchemeReason,
            Name = teamMember.Name,
            TeamMemberId = teamMember.TeamMemberId,
            Version = version,
            HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
        };
    }
}