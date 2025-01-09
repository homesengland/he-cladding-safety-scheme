using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.GetBuildingPartOfDevelopment;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class GetBuildingPartOfDevelopmentTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetBuildingPartOfDevelopmentHandler _handler;

    public GetBuildingPartOfDevelopmentTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new GetBuildingPartOfDevelopmentHandler(_connection.Object, 
                                                              _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        var infoResponse = new GetBuildingPartOfDevelopmentResponse()
        {
            PartOfDevelopment = ENoYes.Yes
        };
        
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetBuildingPartOfDevelopmentResponse>("GetBuildingPartOfDevelopment", It.IsAny<object>()))
                                .ReturnsAsync(infoResponse)
                                .Verifiable();
        
        //// Act
        var result = await _handler.Handle(GetBuildingPartOfDevelopmentRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.PartOfDevelopment == ENoYes.Yes));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
