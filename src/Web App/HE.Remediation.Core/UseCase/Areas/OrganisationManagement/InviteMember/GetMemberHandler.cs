using Mediator;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.OrganisationManagement.InviteMember
{
    public class GetMemberHandler : IRequestHandler<GetMemberRequest, GetMemberResponse>
    {
        private readonly IDbConnectionWrapper _connection;

        public GetMemberHandler(IDbConnectionWrapper connection)
        {
            _connection = connection;
        }

        public async ValueTask<GetMemberResponse> Handle(GetMemberRequest request, CancellationToken cancellationToken)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<GetMemberResult>("GetCollaborationOrganisationUser", request);
            var response = new GetMemberResponse()
            {
                CollaborationUserId = result.Id,
                OrganisationId = result.CollaborationOrganisationId,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.EmailAddress,
                ApplicationRole = (EApplicationRole)result.ApplicationRoleId,
                UserStatus = (ECollaborationUserStatus)result.UserStatusId
            };
            return response;
        }
    }

    public class GetMemberResult
    {
        public Guid Id { get; set; }
        public Guid CollaborationOrganisationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int ApplicationRoleId { get; set; }
        public int UserStatusId { get; set; }
    }

    public class GetMemberRequest : IRequest<GetMemberResponse>
    {
        public Guid CollaborationUserId { get; set; }
    }

    public class GetMemberResponse
    {
        public Guid CollaborationUserId { get; set; }
        public Guid OrganisationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public EApplicationRole ApplicationRole { get; set; }
        public ECollaborationUserStatus UserStatus { get; internal set; }
    }

}
