
using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.DeleteTeamMember;

public class DeleteTeamMemberHandler : IRequestHandler<DeleteTeamMemberRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public DeleteTeamMemberHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(DeleteTeamMemberRequest request, CancellationToken cancellationToken)
    {
        await _progressReportingRepository.DeleteProgressReportTeamMember(request.TeamMemberId);
        return Unit.Value;
    }
}
