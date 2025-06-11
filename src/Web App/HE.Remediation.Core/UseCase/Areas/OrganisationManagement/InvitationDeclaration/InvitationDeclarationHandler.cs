using MediatR;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication.Collaboration;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Services.GovNotify.Models;

namespace HE.Remediation.Core.UseCase.Areas.OrganisationManagement.InvitationDeclaration
{
    public class InvitationDeclarationHandler : IRequestHandler<InvitationDeclarationRequest, InvitationDeclarationResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IBackgroundCollaborationCommunicationQueue _emailInviteSendQueue;

        public InvitationDeclarationHandler(IDbConnectionWrapper connection, IBackgroundCollaborationCommunicationQueue emailInviteSendQueue)
        {
            _connection = connection;
            _emailInviteSendQueue = emailInviteSendQueue;
        }

        public async Task<InvitationDeclarationResponse> Handle(InvitationDeclarationRequest request, CancellationToken cancellationToken)
        {
            // Confirm invite and get details for invite email
            var response = await _connection.QuerySingleOrDefaultAsync<InvitationDeclarationResponse>(
                "GetCollaborationUserInviteDetails", new { request.CollaborationUserId, request.Auth0UserId }
            );

            // Send the invite email

            var collaborationCommunicationRequest = new CollaborationEmailRequest(
                EEmailType.CollaborationOrganisationInvite,
                response.InvitationEmailAddress,
                new Dictionary<string, string>()
                {
                    { "FirstName", response.InvitationTo },
                    { "RequestorFullName", response.RequestorFullName }
                },
                $"CollaborationOrganisationUsers.Id:{response.CollaborationOrganisationUserId}"
            );

            await _emailInviteSendQueue.QueueAsync(collaborationCommunicationRequest, cancellationToken);

            return response;
        }
    }

    public class InvitationDeclarationRequest : IRequest<InvitationDeclarationResponse>
    {
        public Guid CollaborationUserId { get; set; }
        public string Auth0UserId { get; set; }
    }

    public class InvitationDeclarationResponse
    {
        public string OrganisationName { get; set; }
        public string InvitationTo { get; set; }
        public string InvitationEmailAddress { get; set; }
        public string RequestorFullName { get; set; }
        public Guid CollaborationOrganisationUserId { get; set; }
    }
}
