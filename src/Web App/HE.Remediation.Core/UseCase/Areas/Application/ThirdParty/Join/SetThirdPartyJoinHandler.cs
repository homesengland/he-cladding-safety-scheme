using MediatR;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Join
{
    public class SetThirdPartyJoinHandler : IRequestHandler<SetThirdPartyJoinRequest>
    {
        private readonly IDbConnectionWrapper _connection;

        public SetThirdPartyJoinHandler(IDbConnectionWrapper connection)
        {
            _connection = connection;
        }

        public async Task<Unit> Handle(SetThirdPartyJoinRequest request, CancellationToken cancellationToken)
        {
            await _connection.ExecuteAsync("SetThirdPartyUserInviteResponse", request);
            return Unit.Value;
        }
    }

    public class SetThirdPartyJoinRequest : IRequest<Unit>
    {
        public Guid CollaborationUserId { get; set; }
        public Guid ApplicationDetailsId { get; set; }
        public bool IsAccepted { get; set; }
    }
}
