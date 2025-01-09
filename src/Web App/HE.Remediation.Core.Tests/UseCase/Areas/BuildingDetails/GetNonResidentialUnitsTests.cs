using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NonResidentialUnits.GetNonResidentialUnits;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class GetNonResidentialUnitsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetNonResidentialUnitsHandler _handler;

    public GetNonResidentialUnitsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new GetNonResidentialUnitsHandler(_connection.Object, 
                                                     _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        var heightResponse = new GetNonResidentialUnitsResponse()
        {
            NonResidentialUnitsCount = 2
        };
        
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetNonResidentialUnitsResponse>("GetNonResidentialUnits", It.IsAny<object>()))
                                .ReturnsAsync(heightResponse)
        .Verifiable();

        //// Act
        var result = await _handler.Handle(GetNonResidentialUnitsRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.NonResidentialUnitsCount == 2));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
