using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.TeamMember;
public class GetTeamMemberCheckYourAnswersHandler : IRequestHandler<GetTeamMemberCheckYourAnswersRequest, GetTeamMemberCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;

    public GetTeamMemberCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository,
        IApplicationDetailsProvider applicationDetailsProvider)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
        _applicationDetailsProvider = applicationDetailsProvider;
    }

    public async ValueTask<GetTeamMemberCheckYourAnswersResponse> Handle(GetTeamMemberCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var applicationDetails = await _applicationDetailsProvider.GetApplicationDetails();
        var applicationReferenceNumber = applicationDetails.ApplicationReferenceNumber;
        var buildingName = applicationDetails.BuildingName;

        var teamMemberParameters = new GetTeamMemberParameters
        {
            TeamMemberId = request.TeamMemberId,
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        };

        var teamMember = await _progressReportingProjectTeamRepository.GetProjectTeamMember(teamMemberParameters);

        if (teamMember == null)
        {
            throw new InvalidOperationException($"Team member with ID {request.TeamMemberId} not found.");
        }

        return new GetTeamMemberCheckYourAnswersResponse
        {
            ApplicationReferenceNumber = applicationReferenceNumber,
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
            TeamMemberId = teamMember.TeamMemberId
        };
    }
}
