using Moq;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.RemoveMember;
using Mediator;
using HE.Remediation.Core.Services.Communication.Collaboration;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Tests.UseCase.Areas.OrganisationManagement.RemoveMember
{
    public class RemoveMemberSetHandlerTests
    {
        private readonly Mock<IDbConnectionWrapper> _mockConnection;
        private readonly Mock<IBackgroundCollaborationCommunicationQueue> _mockEmailSender;
        private readonly RemoveMemberSetHandler _handler;

        public RemoveMemberSetHandlerTests()
        {
            _mockConnection = new Mock<IDbConnectionWrapper>();
            _mockEmailSender = new Mock<IBackgroundCollaborationCommunicationQueue>(); 
            _handler = new RemoveMemberSetHandler(_mockConnection.Object, _mockEmailSender.Object);
        }

        [Fact]
        public async Task Handle_ShouldExecuteQuerySuccessfully()
        {
            // Arrange
            var request = new RemoveMembersSetRequest(Guid.NewGuid())
            {
                OrganisationId = Guid.NewGuid(),
                AdminUserId = "Admin123"
            };

            var response = new RemoveMembersSetResponse()
            {
                CollaborationUserId = Guid.Empty,
                AdminEmailAddress = "admin@admin.com",
                MemberFirstName = "Bob",
                OrganisationName = "TestOrg",
                MemberEmailAddress = "user@user.org"
            };

            _mockConnection.Setup(conn => conn.QuerySingleOrDefaultAsync<RemoveMembersSetResponse>(
                "SetCollaborationUserToRemoved",
                It.IsAny<object>()))
            .ReturnsAsync(response); // Simulate successful execution

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            _mockConnection.Verify(conn => conn.QuerySingleOrDefaultAsync<RemoveMembersSetResponse>(
                "SetCollaborationUserToRemoved",
                It.IsAny<object>()), Times.Once);

            _mockEmailSender.Verify(send => send.QueueAsync(
                It.Is<CollaborationEmailRequest>(req =>
                    req.EmailType == EEmailType.CollaborationOrganisationUserRemoval &&
                    req.Parameters["FirstName"] == response.MemberFirstName && 
                    req.Parameters["OrganisationName"] == response.OrganisationName && 
                    req.Parameters["AdminUserEmailAddress"] == response.AdminEmailAddress && 
                    req.EmailTo == response.MemberEmailAddress
                ),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
