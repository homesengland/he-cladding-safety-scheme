using HE.Remediation.Core.Services.GovNotify.Models;

namespace HE.Remediation.Core.Services.GovNotify
{
    public interface IGovNotifyService
    {
        public Task<GovNotifyEmailResponseModel> SendEmailAsync<TPersonalisationParameters>(GovNotifyEmailRequestModel<TPersonalisationParameters> model) 
            where TPersonalisationParameters : GlobalPersonalisationParameters;

        public Task<GovNotifyNotificationStatusResponseModel> GetStatusForNotification(Guid notificationId, string accessToken = null);
    }
}
