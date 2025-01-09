using HE.Remediation.Core.Services.GovNotify.Models;

namespace HE.Remediation.Core.Services.GovNotify
{
    public interface IGovNotifyService
    {
        public Task<GovNotifyEmailResponseModel> SendEmailAsync(GovNotifyEmailRequestModel model);

        public Task<GovNotifyNotificationStatusResponseModel> GetStatusForNotification(Guid notificationId, string accessToken = null);
    }
}
