using MediatR;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Join
{
    public class GetThirdPartyJoinHandler : IRequestHandler<GetThirdPartyJoinRequest, GetThirdPartyJoinResponse>
    {
        private readonly IDbConnectionWrapper _connection;

        public GetThirdPartyJoinHandler(IDbConnectionWrapper connection)
        {
            _connection = connection;
        }

        public async Task<GetThirdPartyJoinResponse> Handle(GetThirdPartyJoinRequest request, CancellationToken cancellationToken)
        {
            var response = await _connection.QuerySingleOrDefaultAsync<GetThirdPartyJoinResponse>("GetThirdPartyUserInviteByAuth0UserId", request);
            return response;
        }
    }

    public class GetThirdPartyJoinRequest : IRequest<GetThirdPartyJoinResponse>
    {
        public string Auth0UserId { get; set; }
    }

    public class GetThirdPartyJoinResponse
    {
        public Guid UserId { get; set; }
        public Guid CollaborationUserId { get; set; }
        public Guid ApplicationDetailsId { get; set; }
        public string RequestorFullName { get; set; }
    }

}
