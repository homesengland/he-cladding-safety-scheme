using Moq;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.InviteMember;
using HE.Remediation.Core.Services.Communication.Collaboration;
using static HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.InviteMember.SetInviteMemberHandler;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Invite;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Application
{

    public class SetInviteMemberHandlerTests
    {
        private readonly Mock<IThirdPartyCollaboratorRepository> _thirdPartyCollaboratorRepository = new();
        private readonly Mock<IDbConnectionWrapper> _connectionMock = new();
        private readonly Mock<IApplicationDataProvider> _applicationDataProviderMock = new();
        private readonly Mock<IBackgroundCollaborationCommunicationQueue> _backgroundCollaborationCommunicationQueueMock = new();

        private readonly SetInviteMemberHandler _handler;
        private readonly ThirdPartyInviteResponse _mockResponse;

        public SetInviteMemberHandlerTests()
        {
            _handler = new SetInviteMemberHandler(
                _connectionMock.Object,
                _applicationDataProviderMock.Object,
                _backgroundCollaborationCommunicationQueueMock.Object,
                _thirdPartyCollaboratorRepository.Object
            );

            _mockResponse = new ThirdPartyInviteResponse
            {
                Address = "An Address",
                BuildingName = "A Building name",
                CollaborationOrganisationUserId = Guid.NewGuid(),
                InvitationTo = "To Person",
                RequestorFullName = "Requestor Name"
            };
        }

        [Fact]
        public async Task Handle_ShouldInsertThirdPartyCollaborator_AndReturnResponse()
        {
            // Arrange
            var request = new SetInviteMemberRequest(Guid.NewGuid(), string.Empty, Enums.ETeamMemberSource.ProgressReport);
            var teamMemberResult = new GetInviteResponse
            {
                TeamMemberId = request.TeamMemberId,
                Name = "John Doe",
                EmailAddress = "john.doe@example.com",
            };

            _thirdPartyCollaboratorRepository
                .Setup(repo => repo.GetTeamMemberForThirdPartyCollaboration(request.TeamMemberId, request.Source))
                .ReturnsAsync(teamMemberResult);

            _applicationDataProviderMock
                .Setup(app => app.GetApplicationId())
                .Returns(Guid.NewGuid());


            _connectionMock
                .Setup(db => db.QuerySingleOrDefaultAsync<ThirdPartyInviteResponse>(
                    "GetCollaborationThirdPartyUserInviteDetails", It.IsAny<object>()))
                .ReturnsAsync(_mockResponse);

            _connectionMock
                .Setup(conn => conn.ExecuteAsync("UpsertThirdPartyCollaborator", It.IsAny<object>()))
                .Returns(Task.CompletedTask);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.TeamMemberId, response.TeamMemberId);
            Assert.Equal("John Doe", response.InvitedName);

            _thirdPartyCollaboratorRepository.Verify(repo => repo.GetTeamMemberForThirdPartyCollaboration(request.TeamMemberId, Enums.ETeamMemberSource.ProgressReport), Times.Once);
            _connectionMock.Verify(conn => conn.ExecuteAsync("UpsertThirdPartyCollaborator", It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_IfTeamMemberNotFound()
        {
            // Arrange
            var request = new SetInviteMemberRequest(Guid.NewGuid(), string.Empty, Enums.ETeamMemberSource.ProgressReport);

            _thirdPartyCollaboratorRepository
                .Setup(repo => repo.GetTeamMemberForThirdPartyCollaboration(request.TeamMemberId, request.Source))
                .ReturnsAsync((GetInviteResponse?)null); // Simulate not found

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(request, CancellationToken.None).AsTask());
        }
    }
}
