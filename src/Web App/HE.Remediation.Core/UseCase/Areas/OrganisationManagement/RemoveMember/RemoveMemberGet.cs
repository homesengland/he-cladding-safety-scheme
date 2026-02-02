using Mediator;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.UseCase.Areas.OrganisationManagement.RemoveMember
{
    public class RemoveMemberGetHandler(IDbConnectionWrapper connection) : IRequestHandler<RemoveMembersGetRequest, RemoveMembersResponse>
    {
        private readonly IDbConnectionWrapper _connection = connection;

        public async ValueTask<RemoveMembersResponse> Handle(RemoveMembersGetRequest request, CancellationToken cancellationToken)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<RemoveMembersResponse>("GetCollaborationOrganisationUser", new { request.CollaborationUserId });
            return result;
        }
    }

    public class RemoveMembersGetRequest : IRequest<RemoveMembersResponse>
    {
        public RemoveMembersGetRequest(Guid id) => CollaborationUserId = id;

        public Guid CollaborationUserId { get; private set; }
    }

    public class RemoveMembersResponse
    {
        public Guid Id { get; set; }
        public Guid CollaborationOrganisationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
