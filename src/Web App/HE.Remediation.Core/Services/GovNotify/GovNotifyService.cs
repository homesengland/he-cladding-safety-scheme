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

        public async Task<GovNotifyEmailResponseModel> SendEmailAsync<TPersonalisationParameters>(GovNotifyEmailRequestModel<TPersonalisationParameters> model)
            where TPersonalisationParameters : GlobalPersonalisationParameters
        {
            var requestPath = Environment.GetEnvironmentVariable("APIM_NOTIFY_SENDEMAIL_URI");
            using var response = await GetClient().PostAsJsonAsync(requestPath, model, _serializerOptions);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                var errors = await response.Content.ReadAsStringAsync();
                throw new Exception($"GovNotify API call failed. Status code: {response.StatusCode}, Errors: {errors}");
            }

            return await response.Content.ReadFromJsonAsync<GovNotifyEmailResponseModel>();
        }

        public async Task<GovNotifyNotificationStatusResponseModel> GetStatusForNotification(Guid notificationId, string accessToken = null)
        {
            var requestPath = Environment.GetEnvironmentVariable("APIM_NOTIFY_GETSTATUS_URI");
            using var response = await GetClient().GetAsync($"{requestPath}/{notificationId}");

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                var errors = await response.Content.ReadAsStringAsync();
                throw new Exception($"GovNotify API call failed. Status code: {response.StatusCode}, Errors: {errors}");
            }

            return await response.Content.ReadFromJsonAsync<GovNotifyNotificationStatusResponseModel>(_serializerOptions);
        }

        private HttpClient GetClient() => _httpClientFactory.CreateClient(GovNotifyServiceConstants.GovNotifyHttpClientName);
    }
}
