using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatories.GetGrantFundingSignatories;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ResponsibleEntities;

public class GetGrantFundingSignatoriesTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetGrantFundingSignatoriesHandler _handler;

    public GetGrantFundingSignatoriesTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetGrantFundingSignatoriesHandler(_connection.Object, _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        var grantFundingSignatoryId = Guid.NewGuid();

        var grantFundingSignatoriesResponse = new List<GetGrantFundingSignatoryResponse>
        {
            new()
            {
                Id = grantFundingSignatoryId,
                FullName = "Test Name",
                EmailAddress = "test@test.com", Role = "Tester"
            }
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _connection.Setup(x => x.QueryAsync<GetGrantFundingSignatoryResponse>("GetResponsibleEntitiesGrantFundingSignatories", It.IsAny<object>()))
                                    .ReturnsAsync(grantFundingSignatoriesResponse)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(GetGrantFundingSignatoriesRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.Count == 1) &&
                           (result.First().Id == grantFundingSignatoryId) &&
                           (result.First().FullName == "Test Name") &&
                           (result.First().EmailAddress == "test@test.com") &&
                           (result.First().Role == "Tester"));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
