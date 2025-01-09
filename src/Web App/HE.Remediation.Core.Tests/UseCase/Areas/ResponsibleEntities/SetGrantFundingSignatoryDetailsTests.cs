using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatoryDetails.SetGrantFundingSignatoryDetails;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ResponsibleEntities;

public class SetGrantFundingSignatoryDetailsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly SetGrantFundingSignatoryDetailsHandler _handler;

    public SetGrantFundingSignatoryDetailsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new SetGrantFundingSignatoryDetailsHandler(_connection.Object, _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Sets_Grant_Funding_Signatory_Details_For_Insert()
    {
        //Arrange
        var request = new SetGrantFundingSignatoryDetailsRequest
        {
            Id = null
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _connection.Setup(x => x.ExecuteAsync("InsertResponsibleEntitiesGrantFundingSignatoryDetails", It.IsAny<object>()))
                                   .Returns(Task.CompletedTask)
                                   .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        _connection.Verify();
        _applicationDataProvider.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(),
                                               It.IsAny<object>()),
                                               Times.Once);
    }

    [Fact]
    public async Task Handler_Sets_Grant_Funding_Signatory_Details_For_Update()
    {
        //Arrange
        var request = new SetGrantFundingSignatoryDetailsRequest
        {
            Id = Guid.NewGuid()
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _connection.Setup(x => x.ExecuteAsync("UpdateResponsibleEntitiesGrantFundingSignatoryDetails", It.IsAny<object>()))
                                   .Returns(Task.CompletedTask)
                                   .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        _connection.Verify();
        //_applicationDataProvider.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Never);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(),
                                               It.IsAny<object>()),
                                               Times.Once);
    }
}
