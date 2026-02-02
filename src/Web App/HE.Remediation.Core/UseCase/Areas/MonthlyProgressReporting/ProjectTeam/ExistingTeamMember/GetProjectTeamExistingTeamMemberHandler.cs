using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.ExistingTeamMember;

public class GetProjectTeamExistingTeamMemberHandler : IRequestHandler<GetProjectTeamExistingTeamMemberRequest, GetProjectTeamExistingTeamMemberResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;

    public GetProjectTeamExistingTeamMemberHandler(IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository,
        IApplicationDetailsProvider applicationDetailsProvider)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
        _applicationDetailsProvider = applicationDetailsProvider;
    }

    public async ValueTask<GetProjectTeamExistingTeamMemberResponse> Handle(GetProjectTeamExistingTeamMemberRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var applicationDetails = await _applicationDetailsProvider.GetApplicationDetails();
        var applicationReferenceNumber = applicationDetails.ApplicationReferenceNumber;
        var buildingName = applicationDetails.BuildingName;

        var teamMembersParameters = new GetTeamMembersParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        };

        var teamMembers = await _progressReportingProjectTeamRepository.GetProjectTeamMembers(teamMembersParameters);

        return new GetProjectTeamExistingTeamMemberResponse
        {
            TeamRole = request.TeamRole,
            TeamMembers = teamMembers,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}