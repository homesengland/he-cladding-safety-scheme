using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.SetBuildingHeight;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NonResidentialUnits.SetNonResidentialUnits;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class SetNonResidentialUnitsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly SetNonResidentialUnitsHandler _handler;

    public SetNonResidentialUnitsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new SetNonResidentialUnitsHandler(_connection.Object, 
                                                _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Sets_Non_Residential_Units()
    {
        //Arrange                        
        _connection.Setup(x => x.ExecuteAsync("UpdateNonResidentialUnits", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();
                
        //// Act
        var result = await _handler.Handle(new SetNonResidentialUnitsRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Once);
    }
}
