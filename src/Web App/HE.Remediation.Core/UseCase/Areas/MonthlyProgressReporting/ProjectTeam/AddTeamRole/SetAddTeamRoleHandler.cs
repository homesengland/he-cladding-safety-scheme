using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;
using System;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.AddTeamRole;

public class SetAddTeamRoleHandler : IRequestHandler<SetAddTeamRoleRequest, SetAddTeamRoleResponse>
{
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;

    public SetAddTeamRoleHandler(IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository,
        IApplicationDataProvider applicationDataProvider, 
        IApplicationDetailsProvider applicationDetailsProvider)
    {
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
        _applicationDataProvider = applicationDataProvider;
        _applicationDetailsProvider = applicationDetailsProvider;
    }

    public async Task<SetAddTeamRoleResponse> Handle(SetAddTeamRoleRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var applicationDetails = await _applicationDetailsProvider.GetApplicationDetails();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var teamMembersParameters = new GetTeamMembersParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        };
        var teamMembers = await _progressReportingProjectTeamRepository.GetProjectTeamMembers(teamMembersParameters);
        var teamMemberRoles = teamMembers
        .Where(tm => tm.RoleId.HasValue)
        .Select(tm => tm.RoleId.Value);

        return new SetAddTeamRoleResponse
        {
            CanChooseExistingMembers = (teamMembers?.Count ?? 0) > 0
        };
    }
}
