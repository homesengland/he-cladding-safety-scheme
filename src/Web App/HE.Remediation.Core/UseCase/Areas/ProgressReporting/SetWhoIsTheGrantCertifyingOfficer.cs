using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class SetWhoIsTheGrantCertifyingOfficerHandler : IRequestHandler<SetWhoIsTheGrantCertifyingOfficerRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetWhoIsTheGrantCertifyingOfficerHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetWhoIsTheGrantCertifyingOfficerRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _progressReportingRepository.UpdateGrantCertifyingOfficerTeamMember(request.ProjectTeamMemberId!.Value);

        return Unit.Value;
    }
}

public class SetWhoIsTheGrantCertifyingOfficerRequest : IRequest
{
    public Guid? ProjectTeamMemberId { get; set; }
}