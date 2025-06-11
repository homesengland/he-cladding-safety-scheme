using Moq;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.RemoveMember;

namespace HE.Remediation.Core.Tests.UseCase.Areas.OrganisationManagement.RemoveMember
{
    public class RemoveMemberGetHandlerTests
    {
        private readonly Mock<IDbConnectionWrapper> _mockConnection;
        private readonly RemoveMemberGetHandler _handler;

        public RemoveMemberGetHandlerTests()
        {
            _mockConnection = new Mock<IDbConnectionWrapper>();
            _handler = new RemoveMemberGetHandler(_mockConnection.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnValidResponse_WhenUserExists()
        {
            // Arrange
            var request = new RemoveMembersGetRequest(Guid.NewGuid());
            var expectedResponse = new RemoveMembersResponse
            {
                Id = Guid.NewGuid(),
                CollaborationOrganisationId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe"
            };

            _mockConnection.Setup(conn => conn.QuerySingleOrDefaultAsync<RemoveMembersResponse>(
                "GetCollaborationOrganisationUser",
                It.IsAny<object>()))
            .ReturnsAsync(expectedResponse);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Id, result.Id);
            Assert.Equal(expectedResponse.CollaborationOrganisationId, result.CollaborationOrganisationId);
            Assert.Equal(expectedResponse.FirstName, result.FirstName);
            Assert.Equal(expectedResponse.LastName, result.LastName);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var request = new RemoveMembersGetRequest(Guid.NewGuid());

            _mockConnection.Setup(conn => conn.QuerySingleOrDefaultAsync<RemoveMembersResponse>(
                "GetCollaborationOrganisationUser",
                It.IsAny<object>()))
            .ReturnsAsync((RemoveMembersResponse)null);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
