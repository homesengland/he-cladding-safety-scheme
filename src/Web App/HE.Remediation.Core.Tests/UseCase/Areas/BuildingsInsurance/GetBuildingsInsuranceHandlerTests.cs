using Moq;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.GetBuildingsInsurance;
using HE.Remediation.Core.Data.Repositories;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingsInsurance
{
    public class GetBuildingsInsuranceHandlerTests
    {
        private readonly Mock<IApplicationDataProvider> _applicationDataProviderMock;
        private readonly Mock<IBuildingsInsuranceRepository> _buildingsInsuranceRepositoryMock;
        private readonly GetBuildingsInsuranceHandler _handler;

        public GetBuildingsInsuranceHandlerTests()
        {
            _applicationDataProviderMock = new Mock<IApplicationDataProvider>();
            _buildingsInsuranceRepositoryMock = new Mock<IBuildingsInsuranceRepository>();
            _handler = new GetBuildingsInsuranceHandler(_applicationDataProviderMock.Object, _buildingsInsuranceRepositoryMock.Object);
        }

        [Fact]
        public async Task Should_Return_NewResponse_When_No_Data_Found()
        {
            // Arrange
            _applicationDataProviderMock.Setup(x => x.GetApplicationId()).Returns(Guid.NewGuid());

            _buildingsInsuranceRepositoryMock.Setup(x => x.GetBuildingsInsurance(It.IsAny<Guid>())).ReturnsAsync((GetBuildingsInsuranceResponse)null);

            _buildingsInsuranceRepositoryMock.Setup(x => x.GetBuildingInsuranceProviders()).ReturnsAsync([]);

            // Act
            var result = await _handler.Handle(GetBuildingsInsuranceRequest.Request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.InsuranceProviders);
        }

        [Fact]
        public async Task Should_Return_Response_With_Data_When_Data_Is_Found()
        {
            // Arrange
            _applicationDataProviderMock.Setup(x => x.GetApplicationId()).Returns(Guid.NewGuid());

            var insuranceProviders = new List<InsuranceProvider>
        {
            new() { Id = 1, Name = "Provider A", Order = 1 },
            new() { Id = 2, Name = "Provider B", Order = 2 }
        };

            _buildingsInsuranceRepositoryMock.Setup(x => x.GetBuildingsInsurance(It.IsAny<Guid>())).ReturnsAsync(new GetBuildingsInsuranceResponse());

            _buildingsInsuranceRepositoryMock.Setup(x => x.GetBuildingInsuranceProviders()).ReturnsAsync(insuranceProviders);

            // Act
            var result = await _handler.Handle(GetBuildingsInsuranceRequest.Request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(insuranceProviders.Count, result.InsuranceProviders.Count);
            Assert.Equal("Provider A", result.InsuranceProviders[0].Name);
        }
    }
}

