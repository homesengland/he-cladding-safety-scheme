using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Invite;

public class GetInviteHandler(IProgressReportingRepository progressReportingRepository) : IRequestHandler<GetInviteRequest, GetInviteResponse>
{
    private readonly IProgressReportingRepository _progressReportingRepository = progressReportingRepository;

    public async Task<GetInviteResponse> Handle(GetInviteRequest request, CancellationToken cancellationToken)
    {
        var teamMember = await _progressReportingRepository.GetTeamMember(request.TeamMemberId);

        return new GetInviteResponse
        {
            TeamMemberId = teamMember.TeamMemberId,
            InvitedName = teamMember.Name,
            InvitedEmailAddress = teamMember.EmailAddress
        };
    }
}
