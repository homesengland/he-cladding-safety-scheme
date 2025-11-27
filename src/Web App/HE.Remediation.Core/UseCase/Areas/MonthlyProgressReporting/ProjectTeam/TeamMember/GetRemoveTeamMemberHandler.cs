using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.TeamMember;
public class GetRemoveTeamMemberHandler : IRequestHandler<GetRemoveTeamMemberRequest, GetRemoveTeamMemberResponse>
{
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetRemoveTeamMemberHandler(IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository,
        IApplicationDetailsProvider applicationDetailsProvider,
        IApplicationDataProvider applicationDataProvider)
    {
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
        _applicationDetailsProvider = applicationDetailsProvider;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetRemoveTeamMemberResponse> Handle(GetRemoveTeamMemberRequest request, CancellationToken cancellationToken)
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
        var teamMemberName = teamMember.Name;
        var teamMemberRole = teamMember.Role;
        return new GetRemoveTeamMemberResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            TeamMemberId = request.TeamMemberId,
            TeamMemberName = teamMemberName,
            Role = teamMemberRole
        };
    }
}