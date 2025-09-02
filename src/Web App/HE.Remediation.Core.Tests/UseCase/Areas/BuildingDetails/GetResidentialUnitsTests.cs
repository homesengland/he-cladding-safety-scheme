using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.Dashboard.SchemeSelection;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResidentialUnits.GetResidentialUnits;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class GetResidentialUnitsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetResidentialUnitsHandler _handler;

    public GetResidentialUnitsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new GetResidentialUnitsHandler(_connection.Object, 
                                                _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange
        var unitsResponse = new GetResidentialUnitsResponse()
        {
            ResidentialUnitsCount = 2,
            NonResidentialUnits = ENoYes.Yes
        };
        
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _applicationDataProvider
            .Setup(provider => provider.GetApplicationScheme())
            .Returns(EApplicationScheme.CladdingSafetyScheme);

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetResidentialUnitsResponse>("GetResidentialUnits", It.IsAny<object>()))
                                .ReturnsAsync(unitsResponse)
        .Verifiable();

        //// Act
        var result = await _handler.Handle(GetResidentialUnitsRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.ResidentialUnitsCount == 2));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
