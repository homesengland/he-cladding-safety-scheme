using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperInBusiness.GetDeveloperInBusiness;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class GetDeveloperInBusinessTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetDeveloperInBusinessHandler _handler;

    public GetDeveloperInBusinessTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new GetDeveloperInBusinessHandler(_connection.Object, 
                                                     _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange                
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<int?>("GetDeveloperInBusiness", It.IsAny<object>()))
                                .ReturnsAsync((int)Enums.EApplicationDeveloperInBusinessType.Yes)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetDeveloperInBusinessRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.IsOriginalDeveloperStillInBusiness == Enums.EApplicationDeveloperInBusinessType.Yes));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
