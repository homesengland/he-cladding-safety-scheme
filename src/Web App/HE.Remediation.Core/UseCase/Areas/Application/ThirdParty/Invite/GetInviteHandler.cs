using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Invite;

public class GetInviteHandler(IThirdPartyCollaboratorRepository thirdPartyCollaboratorRepository) : IRequestHandler<GetInviteRequest, GetInviteResponse>
{
    private readonly IThirdPartyCollaboratorRepository _thirdPartyCollaboratorRepository = thirdPartyCollaboratorRepository;

    public async Task<GetInviteResponse> Handle(GetInviteRequest request, CancellationToken cancellationToken)
    {
        var result = await _thirdPartyCollaboratorRepository.GetTeamMemberForThirdPartyCollaboration(request.TeamMemberId, request.Source);
        return result;
    }
}
