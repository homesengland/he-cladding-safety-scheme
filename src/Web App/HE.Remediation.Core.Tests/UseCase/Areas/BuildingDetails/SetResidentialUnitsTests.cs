using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResidentialUnits.SetResidentialUnits;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class SetResidentialUnitsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly SetResidentialUnitsHandler _handler;

    public SetResidentialUnitsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        
        _handler = new SetResidentialUnitsHandler(_connection.Object,
                                                  _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Sets_Residential_Units()
    {
        //Arrange                              
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 
        
        _connection.Setup(x => x.ExecuteAsync("UpdateResidentialUnits", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();
                
        ////// Act
        var result = await _handler.Handle(new SetResidentialUnitsRequest 
        { 
            ResidentialUnitsCount = 2,
            NonResidentialUnits = ENoYes.Yes
        }, CancellationToken.None);

        // Assert
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);        
        _connection.Verify();
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Once);
    }
}
