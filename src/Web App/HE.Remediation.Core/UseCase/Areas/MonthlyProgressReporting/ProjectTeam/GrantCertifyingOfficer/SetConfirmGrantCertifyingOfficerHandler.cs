using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class SetConfirmGrantCertifyingOfficerHandler : IRequestHandler<SetConfirmGrantCertifyingOfficerRequest>
{
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public SetConfirmGrantCertifyingOfficerHandler(
        IApplicationDetailsProvider detailsProvider,
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _detailsProvider = detailsProvider;
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async ValueTask<Unit> Handle(SetConfirmGrantCertifyingOfficerRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var progressReportId = _applicationDataProvider.GetProgressReportId();
        await _progressReportingProjectTeamRepository.ConfirmGrantCertifyingOfficer(progressReportId);

        return Unit.Value;
    }
}

public class SetConfirmGrantCertifyingOfficerRequest : IRequest
{
}