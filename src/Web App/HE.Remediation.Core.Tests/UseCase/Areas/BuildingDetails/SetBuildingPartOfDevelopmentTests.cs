using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.SetBuildingPartOfDevelopment;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class SetBuildingPartOfDevelopmentTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    
    private readonly SetBuildingPartOfDevelopmentHandler _handler;
        
    public SetBuildingPartOfDevelopmentTests()
    {       
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
                
        _handler = new SetBuildingPartOfDevelopmentHandler(_connection.Object,
                                                              _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Sets_Building_Part_Of_Development()
    {
        //Arrange                
        _connection.Setup(x => x.ExecuteAsync("UpdateBuildingPartOfDevelopment", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();
                
        //// Act
        var result = await _handler.Handle(new SetBuildingPartOfDevelopmentRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Once);
    }
}
