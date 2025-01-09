using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.ExistingApplication.GetExistingApplication;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Application;

public class GetExistingApplicationTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly GetExistingApplicationHandler _handler;

    public GetExistingApplicationTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetExistingApplicationHandler(_connection.Object, _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Empty_DB_Result()
    {
        //Arrange
        _connection.Setup(x => x.QueryAsync<GetExistingApplicationResponse>("GetExistingApplications", It.IsAny<object>()))
            .ReturnsAsync(() => null)
            .Verifiable();
            
        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        //// Act
        var result = await _handler.Handle(new GetExistingApplicationRequest(), CancellationToken.None);

        // Assert
        Assert.Null(result);
        //Assert.False(result.FirstOrDefault().Id != firstGuid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }

        [Fact]
    public async Task Handler_Returns_DB_Result()
    {
        //Arrange
        _connection.Setup(x => x.QueryAsync<GetExistingApplicationResponse>("GetExistingApplications", It.IsAny<object>()))
            .ReturnsAsync(new []
            {
                new GetExistingApplicationResponse
                {
                    ApplicationId = Guid.NewGuid()
                },
                new GetExistingApplicationResponse
                {
                    ApplicationId = Guid.NewGuid()
                },
            })
            .Verifiable();
            
        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        //// Act
        var result = await _handler.Handle(new GetExistingApplicationRequest(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        //Assert.False(result.FirstOrDefault().Id != firstGuid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
