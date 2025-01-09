using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperInBusiness.SetDeveloperInBusiness;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class SetDeveloperInBusinessTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly SetDeveloperInBusinessHandler _handler;

    public SetDeveloperInBusinessTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new SetDeveloperInBusinessHandler(_connection.Object, 
                                                     _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Sets_Developer_In_Business()
    {
        //Arrange                        
        _connection.Setup(x => x.ExecuteAsync("UpdateDeveloperInBusiness", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();
                
        //// Act
        var result = await _handler.Handle(new SetDeveloperInBusinessRequest 
        { 
            IsOriginalDeveloperStillInBusiness = Enums.EApplicationDeveloperInBusinessType.Yes
        }, CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Once);
    }
}
