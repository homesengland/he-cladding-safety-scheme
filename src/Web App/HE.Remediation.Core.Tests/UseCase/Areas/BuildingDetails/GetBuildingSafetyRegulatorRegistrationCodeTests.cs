using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingSafetyRegulatorRegistrationCode.GetBuildingSafetyRegulatorRegistrationCode;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class GetBuildingSafetyRegulatorRegistrationCodeTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetBuildingSafetyRegulatorRegistrationCodeHandler _handler;

    public GetBuildingSafetyRegulatorRegistrationCodeTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new GetBuildingSafetyRegulatorRegistrationCodeHandler(_connection.Object,
                                                   _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange                
        GetBuildingSafetyRegulatorRegistrationCodeResponse buildingSafetyRegulatorRegistrationCodeResponse = new GetBuildingSafetyRegulatorRegistrationCodeResponse
        {
            BuildingSafetyRegulatorRegistrationCode = "my registration code"
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetBuildingSafetyRegulatorRegistrationCodeResponse>("GetBuildingSafetyRegulatorRegistrationCode", It.IsAny<object>()))
                                .ReturnsAsync(buildingSafetyRegulatorRegistrationCodeResponse)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetBuildingSafetyRegulatorRegistrationCodeRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.BuildingSafetyRegulatorRegistrationCode == "my registration code"));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
