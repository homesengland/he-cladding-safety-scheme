using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.GetBuildingDeveloperInformation;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class GetBuildingDeveloperInformationTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetBuildingDeveloperInformationHandler _handler;

    public GetBuildingDeveloperInformationTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new GetBuildingDeveloperInformationHandler(_connection.Object, 
                                                              _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        var infoResponse = new GetBuildingDeveloperInformationResponse()
        {
            DoYouKnowOriginalDeveloper = true
        };
        
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationScheme())
                        .Returns(Enums.EApplicationScheme.ResponsibleActorsScheme)
                        .Verifiable();

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetBuildingDeveloperInformationResponse>("GetBuildingOriginalDeveloperIsKnown", It.IsAny<object>()))
                                .ReturnsAsync(infoResponse)
                                .Verifiable();
        
        //// Act
        var result = await _handler.Handle(GetBuildingDeveloperInformationRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.DoYouKnowOriginalDeveloper.Value));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
    
}
