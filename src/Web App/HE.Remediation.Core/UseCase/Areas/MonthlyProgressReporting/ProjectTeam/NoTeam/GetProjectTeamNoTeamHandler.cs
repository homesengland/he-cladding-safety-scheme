using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.NoTeam;
public class GetProjectTeamNoTeamHandler : IRequestHandler<GetProjectTeamNoTeamRequest, GetProjectTeamNoTeamResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;

    public GetProjectTeamNoTeamHandler(IApplicationDataProvider applicationDataProvider, 
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository, 
        IApplicationDetailsProvider applicationDetailsProvider)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
        _applicationDetailsProvider = applicationDetailsProvider;
    }

    public async ValueTask<GetProjectTeamNoTeamResponse> Handle(GetProjectTeamNoTeamRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var parameters = new GetProjectTeamNoTeamParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        };
        var projectTeam = await _progressReportingProjectTeamRepository.GetProjectTeamNoTeam(parameters);
        var applicationDetails = _applicationDetailsProvider.GetApplicationDetails().Result;
        var reference = applicationDetails.ApplicationReferenceNumber;
        var buildingName = applicationDetails.BuildingName;
        return new GetProjectTeamNoTeamResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = reference,
            ReasonNoTeam = projectTeam?.ReasonNoTeam
        };
    }
}
