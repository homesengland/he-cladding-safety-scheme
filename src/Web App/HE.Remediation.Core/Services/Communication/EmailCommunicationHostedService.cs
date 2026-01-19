using HE.Remediation.Core.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.Services.Communication
{
    internal class EmailCommunicationHostedService : BackgroundService
    {
        public IBackgroundEmailCommunicationQueue Queue { get; }
        public IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<EmailCommunicationHostedService> _logger;

        public EmailCommunicationHostedService(
            IBackgroundEmailCommunicationQueue queue,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<EmailCommunicationHostedService> logger)
        {
            Queue = queue ?? throw new ArgumentNullException(nameof(queue));
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Email Communication Hosted Service is running.");

            await ProcessQueue(stoppingToken);
        }

        private async Task ProcessQueue(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var sendEmailCommunicationTask = await Queue.DequeueAsync(cancellationToken);

                _logger.LogInformation("Sending email communication ({TraceId})", sendEmailCommunicationTask.TraceId);

                try
                {
                    await SendEmailCommunication(sendEmailCommunicationTask.EmailCommunicationRequest, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Sending email communication ({TraceId}) failed.", sendEmailCommunicationTask.TraceId);
                }

                _logger.LogInformation("Email communication ({TraceId}) sent successfully.", sendEmailCommunicationTask.TraceId);
            }
        }

        private async Task SendEmailCommunication(EmailCommunicationRequest emailCommunicationRequest, CancellationToken cancellationToken)
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                ICommunicationService _communicationService =
                    scope.ServiceProvider.GetRequiredService<ICommunicationService>();

                await _communicationService.InsertEmailCommunication(emailCommunicationRequest.ApplicationId, emailCommunicationRequest.EmailType);
            }
        }
    }
}
