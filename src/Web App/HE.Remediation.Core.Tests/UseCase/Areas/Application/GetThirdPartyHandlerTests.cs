using Moq;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Get;
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Application
{
    public class GetThirdPartyHandlerTests
    {
        private readonly Mock<IApplicationDataProvider> _applicationDataProviderMock;
        private readonly Mock<IApplicationRepository> _applicationRepositoryMock;
        private readonly Mock<IBuildingDetailsRepository> _buildingDetailsRepositoryMock;
        private readonly Mock<IProgressReportingRepository> _workPackageRepositoryMock;
        private readonly GetThirdPartyHandler _handler;

        public GetThirdPartyHandlerTests()
        {
            _applicationDataProviderMock = new Mock<IApplicationDataProvider>();
            _applicationRepositoryMock = new Mock<IApplicationRepository>();
            _buildingDetailsRepositoryMock = new Mock<IBuildingDetailsRepository>();
            _workPackageRepositoryMock = new Mock<IProgressReportingRepository>();

            _handler = new GetThirdPartyHandler(
                _applicationDataProviderMock.Object,
                _buildingDetailsRepositoryMock.Object,
                _applicationRepositoryMock.Object,
                _workPackageRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Handle_Returns_Correct_Response()
        {
            // Arrange
            var applicationId = Guid.NewGuid();
            var expectedReferenceNumber = "APP-12345";
            var expectedBuildingName = "Test Building";
            var expectedTeamMembers = new List<GetTeamMembersResult>
            {
                new GetTeamMembersResult { Id = Guid.NewGuid(), Name = "Foo", RoleName = "Foo Role" },
                new GetTeamMembersResult { Id = Guid.NewGuid(), Name = "Bar", RoleName = "Bar Role" }
            };

            _applicationDataProviderMock.Setup(x => x.GetApplicationId()).Returns(applicationId);
            _applicationRepositoryMock.Setup(x => x.GetApplicationReferenceNumber(applicationId))
                .ReturnsAsync(expectedReferenceNumber);
            _buildingDetailsRepositoryMock.Setup(x => x.GetBuildingUniqueName(applicationId))
                .ReturnsAsync(expectedBuildingName);
            _workPackageRepositoryMock.Setup(x => x.GetTeamMembers())
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