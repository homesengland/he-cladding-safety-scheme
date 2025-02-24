using HE.Remediation.Core.Services.GovNotify.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace HE.Remediation.Core.Services.GovNotify
{
    public class GovNotifyService : IGovNotifyService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _serializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public GovNotifyService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GovNotifyEmailResponseModel> SendEmailAsync(GovNotifyEmailRequestModel model)
        {
            var requestPath = Environment.GetEnvironmentVariable("APIM_NOTIFY_SENDEMAIL_URI");
            using var response = await GetClient().PostAsJsonAsync(requestPath, model, _serializerOptions);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<GovNotifyEmailResponseModel>();
        }

        public async Task<GovNotifyNotificationStatusResponseModel> GetStatusForNotification(Guid notificationId, string accessToken = null)
        {
            var requestPath = Environment.GetEnvironmentVariable("APIM_NOTIFY_GETSTATUS_URI");
            using var response = await GetClient().GetAsync($"{requestPath}/{notificationId}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<GovNotifyNotificationStatusResponseModel>(_serializerOptions);
        }

        private HttpClient GetClient() => _httpClientFactory.CreateClient(GovNotifyServiceConstants.GovNotifyHttpClientName);
    }
}
