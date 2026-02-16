using Mediator;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.UseCase.Areas.OrganisationManagement.UserOnboarding
{
    public class GetOrgUserInviteHandler : IRequestHandler<GetOrgUserInviteRequest, GetOrgUserInviteResponse>
    {
        private readonly IDbConnectionWrapper _connection;

        public GetOrgUserInviteHandler(IDbConnectionWrapper connection)
        {
            _connection = connection;
        }

        public async ValueTask<GetOrgUserInviteResponse> Handle(GetOrgUserInviteRequest request, CancellationToken cancellationToken)
        {
            var response = await _connection.QuerySingleOrDefaultAsync<GetOrgUserInviteResponse>("GetOrgUserInviteByAuth0UserId", request);
            return response;
        }
    }

    public class GetOrgUserInviteRequest : IRequest<GetOrgUserInviteResponse>
    {
        public string Auth0UserId { get; set; }
    }

    public class GetOrgUserInviteResponse
    {
        public Guid UserId { get; set; }
        public Guid CollaborationUserId { get; set; }
        public Guid CollaborationOrganisationId { get; set; }
        public string RequestorFullName { get; set; }
        public string OrganisationName { get; set; }
    }

}
