using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.NewApplication.CreateNewApplication;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Application;

public class CreateNewApplicationTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly CreateNewApplicationHandler _handler;

    public CreateNewApplicationTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new CreateNewApplicationHandler(_applicationDataProvider.Object, _connection.Object);
    }
    
    [Fact]
    public async Task Create_New_Application()
    {
        //Arrange
        _connection.Setup(x => x.QuerySingleOrDefaultAsync<Guid>("InsertApplication", It.IsAny<object>()))
            .ReturnsAsync(Guid.NewGuid())
            .Verifiable();
            
        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        _applicationDataProvider.Setup(x => x.SetApplicationId(It.IsAny<Guid>()))                                
                                .Verifiable(); 

        //// Act
        var result = await _handler.Handle(CreateNewApplicationRequest.Request, CancellationToken.None);

        // Assert
        //Assert.Null(result);
        //Assert.False(result.FirstOrDefault().Id != firstGuid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
