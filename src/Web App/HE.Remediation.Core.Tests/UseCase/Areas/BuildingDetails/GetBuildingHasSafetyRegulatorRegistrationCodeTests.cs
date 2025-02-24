using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingHasSafetyRegulatorRegistrationCode.GetBuildingHasSafetyRegulatorRegistrationCode;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class GetBuildingHasSafetyRegulatorRegistrationCodeTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetBuildingHasSafetyRegulatorRegistrationCodeHandler _handler;

    public GetBuildingHasSafetyRegulatorRegistrationCodeTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new GetBuildingHasSafetyRegulatorRegistrationCodeHandler(_connection.Object,
                                                _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange                
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<bool?>("GetBuildingHasSafetyRegulatorRegistrationCode", It.IsAny<object>()))
                                .ReturnsAsync(true)
        .Verifiable();

        //// Act
        var result = await _handler.Handle(GetBuildingHasSafetyRegulatorRegistrationCodeRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.BuildingHasSafetyRegulatorRegistrationCode.Value));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
