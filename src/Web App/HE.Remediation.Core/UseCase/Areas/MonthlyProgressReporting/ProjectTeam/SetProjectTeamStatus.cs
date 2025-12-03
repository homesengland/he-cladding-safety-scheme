using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam;

public class SetProjectTeamStatusHandler : IRequestHandler<SetProjectTeamStatusRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public SetProjectTeamStatusHandler(
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async Task<Unit> Handle(SetProjectTeamStatusRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var progressReportId = _applicationDataProvider.GetProgressReportId();
        await _progressReportingProjectTeamRepository.UpdateProjectTeamStatus(progressReportId, request.Status);

        return Unit.Value;
    }
}

public class SetProjectTeamStatusRequest(ETaskStatus status) : IRequest
{
    public ETaskStatus Status { get; set; } = status;
}