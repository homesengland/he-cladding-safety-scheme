using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.BuildingDetails.ConfirmBuildingHeight;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.SetBuildingHeight;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class SetBuildingHeightTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IBuildingDetailsRepository> _buildingDetailsRepository;

    private readonly SetBuildingHeightHandler _handler;

    public SetBuildingHeightTests()
    {
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _buildingDetailsRepository = new Mock<IBuildingDetailsRepository>(MockBehavior.Strict);

        _handler = new SetBuildingHeightHandler(_applicationDataProvider.Object, 
                                                _buildingDetailsRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_Building_Height()
    {
        // Arrange
        var applicationId = Guid.NewGuid();
        var request = new SetBuildingHeightRequest
        {
            NumberOfStoreys = 10,
            BuildingHeight = 30.5m
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(applicationId)
                                .Verifiable();

        _buildingDetailsRepository.Setup(x => x.UpdateBuildingHeight(It.Is<SetBuildingHeightParameters>(p =>
                p.ApplicationId == applicationId &&
                p.NumberOfStoreys == request.NumberOfStoreys &&
                p.BuildingHeight == request.BuildingHeight)))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _buildingDetailsRepository.Verify(x => x.UpdateBuildingHeight(It.IsAny<SetBuildingHeightParameters>()), Times.Once);
    }
}
