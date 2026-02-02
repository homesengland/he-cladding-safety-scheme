using Azure;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication.Collaboration;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.RemoveMember;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.OrganisationManagement.RemoveMember
{
    public class RemoveMemberIntegrationTests : IAsyncDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ServiceCollection _services;
        private readonly Mock<IDbConnectionWrapper> _dbConnectionMock;
        private readonly Mock<ICommunicationService> _communicationServiceMock;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CollaborationCommunicationHostedService _hostedService;
        private readonly RemoveMembersSetResponse _mockResponse;

        public RemoveMemberIntegrationTests()
        {
            _services = new ServiceCollection();
            _dbConnectionMock = new Mock<IDbConnectionWrapper>();
            _communicationServiceMock = new Mock<ICommunicationService>();
            _cancellationTokenSource = new CancellationTokenSource();

            // Setup mock response from database
            _mockResponse = new RemoveMembersSetResponse
            {
                CollaborationUserId = Guid.NewGuid(),
                OrganisationName = "Test Organisation",
                MemberFirstName = "Fred (Invitee)",
                AdminEmailAddress = "from@example.com",
                MemberEmailAddress = "to@example.com",
            };
            _dbConnectionMock.Setup(conn => conn.QuerySingleOrDefaultAsync<RemoveMembersSetResponse>(
                "SetCollaborationUserToRemoved",
                It.IsAny<object>()))
            .ReturnsAsync(_mockResponse);

            // Setup service collection
            _services.AddSingleton(_dbConnectionMock.Object);
            _services.AddSingleton<IBackgroundCollaborationCommunicationQueue, BackgroundCollaborationCommunicationQueue>();
            _services.AddSingleton(_communicationServiceMock.Object);
            _services.AddTransient<RemoveMemberSetHandler>();
            _services.AddLogging(builder => builder.AddConsole());
            _serviceProvider = _services.BuildServiceProvider();
            
            // Create and start the hosted service
            var queue = _serviceProvider.GetRequiredService<IBackgroundCollaborationCommunicationQueue>();
            var scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
            var logger = _serviceProvider.GetRequiredService<ILogger<CollaborationCommunicationHostedService>>();
            
            _hostedService = new CollaborationCommunicationHostedService(
                queue, 
                scopeFactory, 
                logger);

            _hostedService.StartAsync(_cancellationTokenSource.Token).GetAwaiter().GetResult();
        }

        [Fact]
        public async Task InvitationDeclarationHandler_SendsEmailViaHostedService_WhenHandled()
        {
            // ARRANGE

            var handler = _serviceProvider.GetRequiredService<RemoveMemberSetHandler>();

            // ... Setup communication service to capture the request
            var capturedCollaborationEmailRequest = (CollaborationEmailRequest?)null;
            _communicationServiceMock
                .Setup(x => x.SendEmailInvite(It.IsAny<CollaborationEmailRequest>(), It.IsAny<CancellationToken>()))
                .Callback<CollaborationEmailRequest, CancellationToken>((req, _) => capturedCollaborationEmailRequest = req)
                .Returns(Task.CompletedTask);

            // ACT

            var request = new RemoveMembersSetRequest(Guid.NewGuid()) { OrganisationId = Guid.NewGuid(), AdminUserId = "auto0|12345" };
            await handler.Handle(request, CancellationToken.None);

            // ... Wait a bit for the background service to process the queue
            await Task.Delay(500);

            // ASSERT

            // ... Verify the communication service was called with correct data
            _communicationServiceMock.Verify(
                x => x.SendEmailInvite(
                    It.Is<CollaborationEmailRequest>(r => 
                        r.EmailType == EEmailType.CollaborationOrganisationUserRemoval &&
                        r.EmailTo == _mockResponse.MemberEmailAddress), 
                    It.IsAny<CancellationToken>()),
                Times.Once);

            // ... Verify parameters are passed correctly into communication service
            Assert.NotNull(capturedCollaborationEmailRequest);
            var parameters = capturedCollaborationEmailRequest.Parameters;
            Assert.Equal(_mockResponse.OrganisationName, parameters["OrganisationName"]);
            Assert.Equal(_mockResponse.MemberFirstName, parameters["FirstName"]);
            Assert.Equal(_mockResponse.AdminEmailAddress, parameters["AdminUserEmailAddress"]);
        }

        public async ValueTask DisposeAsync()
        {
            // Stop the hosted service
            await _hostedService.StopAsync(_cancellationTokenSource.Token);
            _cancellationTokenSource.Dispose();
            
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
