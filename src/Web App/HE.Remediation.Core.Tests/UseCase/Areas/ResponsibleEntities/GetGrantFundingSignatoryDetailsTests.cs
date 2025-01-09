using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatoryDetails.GetGrantFundingSignatoryDetails;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ResponsibleEntities;

public class GetGrantFundingSignatoryDetailsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetGrantFundingSignatoryDetailsHandler _handler;

    public GetGrantFundingSignatoryDetailsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetGrantFundingSignatoryDetailsHandler(_connection.Object, _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        var grantFundingSignatoryId = Guid.NewGuid();

        var grantFundingSignatoryDetailsResponse = new GetGrantFundingSignatoryDetailsResponse
        {
            Id = grantFundingSignatoryId,
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            EmailAddress = "test@test.com",
            Role = "Tester"
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetGrantFundingSignatoryDetailsResponse>("GetResponsibleEntitiesGrantFundingSignatoryDetails", It.IsAny<object>()))
                                        .ReturnsAsync(grantFundingSignatoryDetailsResponse)
                                        .Verifiable();

        //// Act
        var result = await _handler.Handle(new GetGrantFundingSignatoryDetailsRequest(), CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.Id == grantFundingSignatoryId) &&
                           (result.FirstName == "TestFirstName") &&
                           (result.LastName == "TestLastName") &&
                           (result.EmailAddress == "test@test.com") &&
                           (result.Role == "Tester"));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetUserId(), Times.Never);
    }
}
