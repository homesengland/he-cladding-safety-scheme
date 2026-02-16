using Mediator;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.UseCase.Areas.OrganisationManagement.UserOnboarding
{
    public class SetOrgUserInviteHandler : IRequestHandler<SetOrgUserInviteRequest>
    {
        private readonly IDbConnectionWrapper _connection;

        public SetOrgUserInviteHandler(IDbConnectionWrapper connection)
        {
            _connection = connection;
        }

        public async ValueTask<Unit> Handle(SetOrgUserInviteRequest request, CancellationToken cancellationToken)
        {
            await _connection.ExecuteAsync("SetOrgUserInviteResponse", request);
            return Unit.Value;
        }
    }

    public class SetOrgUserInviteRequest : IRequest<Unit>
    {
        public Guid CollaborationUserId { get; set; }
        public bool IsAccepted { get; set; }
    }
}
