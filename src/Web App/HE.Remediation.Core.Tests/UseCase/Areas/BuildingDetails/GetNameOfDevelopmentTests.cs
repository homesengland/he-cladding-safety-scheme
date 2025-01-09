using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.NameOfDevelopment.GetNameOfDevelopment;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class GetNameOfDevelopmentTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetNameOfDevelopmentHandler _handler;

    public GetNameOfDevelopmentTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new GetNameOfDevelopmentHandler(_connection.Object, 
                                                   _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange                
        GetNameOfDevelopmentResponse nameOfDevelopmentResponse = new GetNameOfDevelopmentResponse
        {
            NameOfDevelopment = "my development"
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetNameOfDevelopmentResponse>("GetNameOfDevelopment", It.IsAny<object>()))
                                .ReturnsAsync(nameOfDevelopmentResponse)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetNameOfDevelopmentRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.NameOfDevelopment == "my development"));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
