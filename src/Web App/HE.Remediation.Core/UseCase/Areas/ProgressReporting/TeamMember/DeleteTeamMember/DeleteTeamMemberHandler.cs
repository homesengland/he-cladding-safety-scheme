
using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.DeleteTeamMember;

public class DeleteTeamMemberHandler : IRequestHandler<DeleteTeamMemberRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public DeleteTeamMemberHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(DeleteTeamMemberRequest request, CancellationToken cancellationToken)
    {
        await DeleteTeamMember(request);
        return Unit.Value;
    }

    private async Task DeleteTeamMember(DeleteTeamMemberRequest request)
    {
        await _progressReportingRepository.DeleteProgressReportTeamMember(request.TeamMemberId);
    }
}
