using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.AddTeamRole;

public class GetAddRoleHandler : IRequestHandler<GetAddTeamRoleRequest, GetAddTeamRoleResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public GetAddRoleHandler(IApplicationDataProvider applicationDataProvider,
                                           IApplicationDetailsProvider applicationDetailsProvider,
                                           IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationDetailsProvider = applicationDetailsProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async Task<GetAddTeamRoleResponse> Handle(GetAddTeamRoleRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var applicationDetails = await _applicationDetailsProvider.GetApplicationDetails();
        var applicationReferenceNumber = applicationDetails.ApplicationReferenceNumber;
        var buildingName = applicationDetails.BuildingName;
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var teamMembersParameters = new GetTeamMembersParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        };

        var teamMembers = await _progressReportingProjectTeamRepository.GetProjectTeamMembers(teamMembersParameters);
        var allOptions = Enum.GetValues<ETeamRole>().ToList();
        var consumedOptions = teamMembers
                                .Where(tm => tm.RoleId.HasValue)
                                .Select(tm => (ETeamRole)tm.RoleId.Value);
        var availableOptions = allOptions.Except(consumedOptions).ToList();

        return new GetAddTeamRoleResponse
        {
            BuildingName = buildingName,
            AvailableTeamRoles = availableOptions,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}
