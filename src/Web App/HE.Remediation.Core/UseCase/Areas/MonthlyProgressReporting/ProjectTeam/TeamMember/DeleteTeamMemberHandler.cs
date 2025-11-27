using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.TeamMember;
using MediatR;

public class DeleteTeamMemberHandler : IRequestHandler<DeleteTeamMemberRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public DeleteTeamMemberHandler(IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async Task<Unit> Handle(DeleteTeamMemberRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var parameters = new DeleteProjectTeamTeamMemberParameters
        {
            TeamMemberId = request.TeamMemberId,
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        };
        await _progressReportingProjectTeamRepository.DeleteProjectTeamTeamMember(parameters);
        return Unit.Value;
    }
}
