using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Services.GovNotify.Models;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.Services.Communication
{
    public class CommunicationConstants
    {
        private readonly GovNotifySettings _govNotifySettings;

        public CommunicationConstants(IOptions<GovNotifySettings> govNotifySettings)
        {
            _govNotifySettings = govNotifySettings.Value;
        }

        public Guid GetEmailTypeTemplateId(EEmailType emailType)
        {
            switch (emailType)
            {
                case EEmailType.ApplicationSubmitted:
                    return _govNotifySettings.ApplicationSubmittedEmailTemplateId;
                case EEmailType.SssfBuildingCompleteApplicationSubmitted:
                    return _govNotifySettings.SssfBuildingCompleteApplicationSubmittedTemplateId;
                case EEmailType.SssfBuildingNonCompleteApplicationSubmitted:
                    return _govNotifySettings.SssfBuildingNonCompleteApplicationSubmittedEmailTemplateId;
                case EEmailType.WorksPackageSubmitted:
                    return _govNotifySettings.WorksPackageSubmittedEmailTemplateId;
                case EEmailType.ScheduleOfWorksSubmitted:
                    return _govNotifySettings.ScheduleOfWorksSubmittedEmailTemplateId;
                case EEmailType.PaymentRequestSubmitted:
                    return _govNotifySettings.PaymentRequestSubmittedEmailTemplateId;
                case EEmailType.VariationSubmitted:
                    return _govNotifySettings.VariationSubmittedEmailTemplateId;
                case EEmailType.ClosingReportSubmitted:
                    return _govNotifySettings.ClosingReportSubmittedEmailTemplateId;
                case EEmailType.CollaborationOrganisationInvite:
                    return _govNotifySettings.CollaborationOrganisationInviteEmailTemplateId;
                case EEmailType.CollaborationOrganisationUserRemoval:
                    return _govNotifySettings.CollaborationOrganisationInviteRemovalEmailTemplateId;
                case EEmailType.CollaborationThirdPartyInvite:
                    return _govNotifySettings.CollaborationThirdPartyInviteTemplateId;
                case EEmailType.CollaborationThirdPartyRemoveAccess:
                    return _govNotifySettings.CollaborationThirdPartyInviteRemovalEmailTemplateId;
                default:
                    throw new Exception("Email type not supported");
            }
        }

        public string GetEmailTypeName(EEmailType emailType)
        {
            switch (emailType)
            {
                case EEmailType.ApplicationSubmitted:
                case EEmailType.SssfBuildingCompleteApplicationSubmitted:
                case EEmailType.SssfBuildingNonCompleteApplicationSubmitted:
                    return "Apply for grant - Application submitted confirmation";
                case EEmailType.WorksPackageSubmitted:
                    return "Works Package - Works Package submitted confirmation";
                case EEmailType.ScheduleOfWorksSubmitted:
                    return "Works Delivery - Schedule of Works submitted confirmation";
                case EEmailType.PaymentRequestSubmitted:
                    return "Works Delivery - Payment request submitted confirmation";
                case EEmailType.VariationSubmitted:
                    return "Works Delivery - Variation submitted confirmation";
                case EEmailType.ClosingReportSubmitted:
                    return "Works Complete - Closing Report submitted confirmation";
                default:
                    throw new Exception("Email type not supported");
            }
        }

        public int GetEmailTypeCategory(EEmailType emailType)
        {
            switch (emailType)
            {
                case EEmailType.ApplicationSubmitted:
                    return (int)ECommunicationCategory.ApplyForGrant;
                case EEmailType.WorksPackageSubmitted:
                    return (int)ECommunicationCategory.WorksPackage;
                case EEmailType.ScheduleOfWorksSubmitted:
                    return (int)ECommunicationCategory.WorksStarted;
                case EEmailType.PaymentRequestSubmitted:
                    return (int)ECommunicationCategory.WorksStarted;
                case EEmailType.VariationSubmitted:
                    return (int)ECommunicationCategory.WorksStarted;
                case EEmailType.ClosingReportSubmitted:
                    return (int)ECommunicationCategory.WorksComplete;
                default:
                    throw new Exception("Email type not supported");
            }
        }
    }
}
