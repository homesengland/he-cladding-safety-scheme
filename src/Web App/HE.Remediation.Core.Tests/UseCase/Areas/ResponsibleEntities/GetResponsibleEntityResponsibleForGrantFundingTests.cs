using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.EntityResponsibleForGFA.GetResponsibleEntityResponsibleForGrantFunding;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ResponsibleEntities;

public class GetResponsibleEntityResponsibleForGrantFundingTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetResponsibleEntityResponsibleForGrantFundingHandler _handler;

    public GetResponsibleEntityResponsibleForGrantFundingTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetResponsibleEntityResponsibleForGrantFundingHandler(_connection.Object, _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        var responsibleForGrantFundingResponse = new GetResponsibleEntityResponsibleForGrantFundingResponse
        {
            ResponsibleForGrantFunding = true
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetResponsibleEntityResponsibleForGrantFundingResponse>("GetResponsibleEntityResponsibleForGrantFunding", It.IsAny<object>()))
                                    .ReturnsAsync(responsibleForGrantFundingResponse)
                                    .Verifiable();
        //// Act
        var result = await _handler.Handle(GetResponsibleEntityResponsibleForGrantFundingRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.ResponsibleForGrantFunding == true));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
