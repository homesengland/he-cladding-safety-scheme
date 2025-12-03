using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class SetWhoIsTheGrantCertifyingOfficerHandler : IRequestHandler<SetWhoIsTheGrantCertifyingOfficerRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public SetWhoIsTheGrantCertifyingOfficerHandler(
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async Task<Unit> Handle(SetWhoIsTheGrantCertifyingOfficerRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        await _progressReportingProjectTeamRepository.UpdateGrantCertifyingOfficerTeamMember(progressReportId, request.ProjectTeamMemberId!.Value);

        return Unit.Value;
    }
}

public class SetWhoIsTheGrantCertifyingOfficerRequest : IRequest
{
    public Guid? ProjectTeamMemberId { get; set; }
}