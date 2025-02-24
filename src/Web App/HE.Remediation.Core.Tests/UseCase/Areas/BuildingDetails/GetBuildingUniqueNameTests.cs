using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.GetBuildingUniqueName;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class GetBuildingUniqueNameTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;    
    private readonly Mock<IBuildingDetailsRepository> _buildingDetailsRepository;

    private readonly GetBuildingUniqueNameHandler _handler;

    public GetBuildingUniqueNameTests()
    {
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _buildingDetailsRepository = new Mock<IBuildingDetailsRepository>(MockBehavior.Strict);

        _handler = new GetBuildingUniqueNameHandler(_applicationDataProvider.Object,
                                                    _buildingDetailsRepository.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        string buildingName = "my building";
        _buildingDetailsRepository.Setup(x => x.GetBuildingUniqueName(It.IsAny<Guid>()))
                                .ReturnsAsync(buildingName)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetBuildingUniqueNameRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.UniqueName == buildingName));
        Assert.True(resultValid);
        _applicationDataProvider.Verify();
    }
}
