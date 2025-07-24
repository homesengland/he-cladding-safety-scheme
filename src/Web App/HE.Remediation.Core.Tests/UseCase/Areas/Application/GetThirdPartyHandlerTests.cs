using Moq;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Get;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Application
{
    public class GetThirdPartyHandlerTests
    {
        private readonly Mock<IApplicationDataProvider> _applicationDataProviderMock = new();
        private readonly Mock<IThirdPartyCollaboratorRepository> _thirdPartyCollaboratorRepository = new();
        private readonly GetThirdPartyHandler _handler;

        public GetThirdPartyHandlerTests()
        { 
            _handler = new GetThirdPartyHandler(
                _applicationDataProviderMock.Object,
                _thirdPartyCollaboratorRepository.Object
            );
        }

        [Fact]
        public async Task Handle_Returns_Correct_Response()
        {
            // Arrange
            var applicationId = Guid.NewGuid();
            var expectedReferenceNumber = "APP-12345";
            var expectedBuildingName = "Test Building";
            var expectedTeamMembers = new GetThirdPartyResponse
            {
                ApplicationReferenceNumber = expectedReferenceNumber,
                BuildingName = expectedBuildingName,
                TeamMembers = new List<GetThirdPartyResponse.TeamMember>()
                {
                    new GetThirdPartyResponse.TeamMember
                    {
                        Id = Guid.NewGuid(),
                        Name = "Alice",
                        RoleName = "Architect",
                        InviteStatus = ECollaborationUserStatus.Invited
                    },
                    new GetThirdPartyResponse.TeamMember
                    {
                        Id = Guid.NewGuid(),
                        Name = "Bob",
                        RoleName = "Engineer",
                        InviteStatus = ECollaborationUserStatus.Invited
                    }
                }
            };

            _applicationDataProviderMock.Setup(x => x.GetApplicationId()).Returns(applicationId);

            _thirdPartyCollaboratorRepository.Setup(x => x.GetTeamMembersForThirdPartyCollaboration(applicationId))
                .ReturnsAsync(expectedTeamMembers);

            var expectedMissingRoles = new List<ETeamRole>
            {
                ETeamRole.FireSafetyEngineer,
                ETeamRole.LeadDesigner,
                ETeamRole.LeadContractor
            };

            // Act
            var result = await _handler.Handle(GetThirdPartyRequest.Request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedReferenceNumber, result.ApplicationReferenceNumber);
            Assert.Equal(expectedBuildingName, result.BuildingName);
        }
    }
}