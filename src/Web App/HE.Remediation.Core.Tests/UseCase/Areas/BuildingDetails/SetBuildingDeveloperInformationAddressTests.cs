using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.SetBuildingDeveloperAddressInformation;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class SetBuildingDeveloperInformationAddressTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    
    private readonly SetBuildingDeveloperInformationAddressHandler _handler;
        
    public SetBuildingDeveloperInformationAddressTests()
    {       
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
                
        _handler = new SetBuildingDeveloperInformationAddressHandler(_connection.Object,
                                                                     _applicationDataProvider.Object);
    }
    
    [Fact]
    public async Task Handler_Sets_Building_Developer_Address()
    {
        //Arrange                
        _connection.Setup(x => x.ExecuteAsync("UpdateBuildingDeveloperInformation", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();
                
        //// Act
        var result = await _handler.Handle(new SetBuildingDeveloperInformationAddressRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Once);
    }
}
