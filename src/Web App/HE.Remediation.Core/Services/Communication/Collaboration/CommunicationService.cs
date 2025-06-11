using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Services.GovNotify;
using HE.Remediation.Core.Services.GovNotify.Models;

namespace HE.Remediation.Core.Services.Communication.Collaboration
{
    public class CommunicationService : ICommunicationService
    {
        private readonly IGovNotifyService _govNotifyService;
        private readonly CommunicationConstants _communicationConstants;

        public CommunicationService(IGovNotifyService govNotifyService, CommunicationConstants communicationConstants)
        {
            _govNotifyService = govNotifyService;
            _communicationConstants = communicationConstants;
        }

        public async Task SendEmailInvite(CollaborationEmailRequest emailRequest, CancellationToken cancellationToken = default)
        {
            switch (emailRequest.EmailType)
            {
                case EEmailType.CollaborationOrganisationInvite:
                    await SendInviteEmail(emailRequest);
                    break;
                case EEmailType.CollaborationOrganisationUserRemoval:
                    await SendRemovalEmail(emailRequest);
                    break;
            }
        }

        private async Task SendInviteEmail(CollaborationEmailRequest emailRequest)
        {
            await _govNotifyService.SendEmailAsync(new GovNotifyEmailRequestModel<OrganisationInviteParameters>
            {
                TemplateId = _communicationConstants.GetEmailTypeTemplateId(emailRequest.EmailType),
                EmailAddress = emailRequest.EmailTo,
                Personalisation = new OrganisationInviteParameters()
                {
                    FirstName = emailRequest.Parameters["FirstName"],
                    RequestorFullName = emailRequest.Parameters["RequestorFullName"],
                    RecipientEmail = emailRequest.EmailTo
                }
            });
        }

        private async Task SendRemovalEmail(CollaborationEmailRequest emailRequest)
        {
            await _govNotifyService.SendEmailAsync(new GovNotifyEmailRequestModel<OrganisationRemovalParameters>
            {
                TemplateId = _communicationConstants.GetEmailTypeTemplateId(emailRequest.EmailType),
                EmailAddress = emailRequest.EmailTo,
                Personalisation = new OrganisationRemovalParameters()
                {
                    FirstName = emailRequest.Parameters["FirstName"],
                    OrganisationName = emailRequest.Parameters["OrganisationName"],
                    AdminUserEmailAddress = emailRequest.Parameters["AdminUserEmailAddress"],
                    RecipientEmail = emailRequest.EmailTo
                }
            });
        }
    }
}
