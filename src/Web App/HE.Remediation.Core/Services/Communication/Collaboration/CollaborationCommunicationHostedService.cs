using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.Services.Communication.Collaboration
{
    internal class CollaborationCommunicationHostedService : BackgroundService
    {
        private IBackgroundCollaborationCommunicationQueue _queue { get; }
        private IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<CollaborationCommunicationHostedService> _logger;

        public CollaborationCommunicationHostedService(
            IBackgroundCollaborationCommunicationQueue queue,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<CollaborationCommunicationHostedService> logger)
        {
            _queue = queue;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Collaboration Communication Hosted Service is running.");

            await ProcessQueue(stoppingToken);
        }

        private async Task ProcessQueue(CancellationToken cancellationToken)
        {
            await foreach (var item in _queue.ReadAllAsync(cancellationToken))
            {
                try
                {
                    using IServiceScope scope = _serviceScopeFactory.CreateScope();
                    var communicationService = scope.ServiceProvider.GetRequiredService<ICommunicationService>();
                    await communicationService.SendEmailInvite(item, cancellationToken);
                    _logger.LogInformation("Email communication (Type: {EmailType}, Info: {TraceInfo}) sent successfully.", item.EmailType, item.TraceInfo);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Email communication failed (Type: {EmailType}, Info: {TraceInfo}).", item.EmailType, item.TraceInfo);
                }
            }
        }

    }
}
