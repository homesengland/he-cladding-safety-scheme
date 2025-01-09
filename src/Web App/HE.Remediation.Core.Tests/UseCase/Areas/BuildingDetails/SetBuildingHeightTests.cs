using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.SetBuildingHeight;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class SetBuildingHeightTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly SetBuildingHeightHandler _handler;

    public SetBuildingHeightTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new SetBuildingHeightHandler(_connection.Object, 
                                                _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Sets_Building_Height()
    {
        //Arrange                        
        _connection.Setup(x => x.ExecuteAsync("UpdateBuildingHeight", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();
                
        //// Act
        var result = await _handler.Handle(new SetBuildingHeightRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Once);
    }
}
