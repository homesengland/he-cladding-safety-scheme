using Mediator;
using HE.Remediation.Core.Interface;
using Microsoft.Data.SqlClient;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.InviteMember;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Services.Communication.Collaboration;

namespace HE.Remediation.Core.UseCase.Areas.OrganisationManagement.RemoveMember
{
    public class RemoveMemberSetHandler(IDbConnectionWrapper connection, IBackgroundCollaborationCommunicationQueue emailInviteSendQueue) : IRequestHandler<RemoveMembersSetRequest, Unit>
    {
        private readonly IDbConnectionWrapper _connection = connection;
        private readonly IBackgroundCollaborationCommunicationQueue _emailInviteSendQueue = emailInviteSendQueue;

        public async ValueTask<Unit> Handle(RemoveMembersSetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _connection.QuerySingleOrDefaultAsync<RemoveMembersSetResponse>("SetCollaborationUserToRemoved",
                    new { request.CollaborationUserId, request.OrganisationId, request.AdminUserId });

                await SendRemovalEmail(request.CollaborationUserId, response, cancellationToken);
            }
            catch (SqlException ex) when (ex.Message.Contains("Invalid admin user"))
            {
                throw new InvalidAdminOrganisationException();
            }

            return Unit.Value;
        }

        private async Task SendRemovalEmail(Guid collaborationUserId, RemoveMembersSetResponse response, CancellationToken cancellationToken)
        {
            var collaborationCommunicationRequest = 
                new CollaborationEmailRequest(
                    EEmailType.CollaborationOrganisationUserRemoval,
                    response.MemberEmailAddress,
                    new Dictionary<string, string>()
                    {
                        { "FirstName", response.MemberFirstName },
                        { "OrganisationName", response.OrganisationName },
                        { "AdminUserEmailAddress", response.AdminEmailAddress }
                    },
                    $"CollaborationOrganisationUsers.Id:{collaborationUserId}"
                );

            await _emailInviteSendQueue.QueueAsync(collaborationCommunicationRequest, cancellationToken);
        }
    }

    public class RemoveMembersSetRequest : IRequest<Unit>
    {
        public RemoveMembersSetRequest(Guid collaborationUserId) => CollaborationUserId = collaborationUserId;

        public Guid CollaborationUserId { get; private set; }
        public string AdminUserId { get; set; }
        public Guid OrganisationId { get; set; }
    }

    public class RemoveMembersSetResponse 
    {
        public Guid CollaborationUserId { get; set; }
        public string MemberEmailAddress { get; set; }
        public string MemberFirstName { get; set; }
        public string OrganisationName { get; set; }
        public string AdminEmailAddress { get; set; }
    }
}
