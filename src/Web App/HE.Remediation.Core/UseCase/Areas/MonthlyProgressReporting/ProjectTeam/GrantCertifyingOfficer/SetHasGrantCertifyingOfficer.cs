using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class SetHasGrantCertifyingOfficerHandler : IRequestHandler<SetHasGrantCertifyingOfficerRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public SetHasGrantCertifyingOfficerHandler(
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async Task<Unit> Handle(SetHasGrantCertifyingOfficerRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var progressReportId = _applicationDataProvider.GetProgressReportId();
        await _progressReportingProjectTeamRepository.SetHasGrantCertifyingOfficer(progressReportId, request.DoYouHaveAGrantCertifyingOfficer!.Value);

        return Unit.Value;
    }
}

public class SetHasGrantCertifyingOfficerRequest : IRequest
{
    public bool? DoYouHaveAGrantCertifyingOfficer { get; set; }
}