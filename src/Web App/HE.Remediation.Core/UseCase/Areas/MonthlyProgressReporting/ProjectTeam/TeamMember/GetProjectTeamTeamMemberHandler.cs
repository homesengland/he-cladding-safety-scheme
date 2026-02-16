using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.TeamMember;
public class GetProjectTeamTeamMemberHandler : IRequestHandler<GetProjectTeamTeamMemberRequest, GetProjectTeamTeamMemberResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IProgressReportingProjectTeamRepository _projectTeamRepository;
    public GetProjectTeamTeamMemberHandler(IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository,
        IApplicationDetailsProvider applicationDetailsProvider,
        IProgressReportingProjectTeamRepository projectTeamRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
        _applicationDetailsProvider = applicationDetailsProvider;
        _projectTeamRepository = projectTeamRepository;
    }

    public async ValueTask<GetProjectTeamTeamMemberResponse> Handle(GetProjectTeamTeamMemberRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var applicationDetails = await _applicationDetailsProvider.GetApplicationDetails();
        var applicationReferenceNumber = applicationDetails.ApplicationReferenceNumber;
        var buildingName = applicationDetails.BuildingName;

        var teamMemberParameters = new GetTeamMemberParameters
        {
            TeamMemberId = request.TeamMemberId ?? request.Selected,
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        };

        var projectTeam = await _projectTeamRepository.GetProjectTeam(new GetProjectTeamParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });

        var teamMember = await _progressReportingProjectTeamRepository.GetProjectTeamMember(teamMemberParameters);
        return new GetProjectTeamTeamMemberResponse
        {
            Role = request.TeamRole,
            OtherRole = teamMember?.OtherRole,
            ApplicationReferenceNumber = applicationReferenceNumber,
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
            HasChasCertification = teamMember?.HasChasCertification,
            ConsiderateConstructorSchemeReasonNo = teamMember?.ConsiderateConstructorSchemeType == EConsiderateConstructorSchemeType.No ? teamMember.ConsiderateConstructorSchemeReasonNo : null,
            ConsiderateConstructorSchemeReasonDontKnow = teamMember?.ConsiderateConstructorSchemeType == EConsiderateConstructorSchemeType.DontKnow ? teamMember.ConsiderateConstructorSchemeReasonDontKnow : null,
            GrantCertifyingOfficerTeamMemberId = projectTeam?.GrantCertifyingOfficerTeamMemberId
        };
    }
}