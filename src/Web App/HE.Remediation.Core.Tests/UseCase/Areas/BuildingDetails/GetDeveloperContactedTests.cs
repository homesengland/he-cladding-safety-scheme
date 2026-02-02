using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperContacted.GetDeveloperContacted;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class GetDeveloperContactedTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetDeveloperContactedHandler _handler;

    public GetDeveloperContactedTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new GetDeveloperContactedHandler(_connection.Object, 
                                                _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange                
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<bool?>("GetDeveloperContacted", It.IsAny<object>()))
                                .ReturnsAsync(true)
        .Verifiable();

        //// Act
        var result = await _handler.Handle(GetDeveloperContactedRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result?.HasDeveloperBeenContactedAboutRemediation != null) &&
                           (result.HasDeveloperBeenContactedAboutRemediation.Value));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
