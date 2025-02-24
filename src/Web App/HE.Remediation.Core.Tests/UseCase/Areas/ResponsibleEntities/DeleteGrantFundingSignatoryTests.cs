using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatories.DeleteGrantFundingSignatory;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ResponsibleEntities;

public class DeleteGrantFundingSignatoryTests
{
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly DeleteGrantFundingSignatoryHandler _handler;

    public DeleteGrantFundingSignatoryTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _handler = new DeleteGrantFundingSignatoryHandler(_connection.Object);
    }

    [Fact]
    public async Task Handler_Deletes_Grant_Funding_Signatory()
    {
        //Arrange
        _connection.Setup(x => x.ExecuteAsync("DeleteResponsibleEntitiesGrantFundingSignatory", It.IsAny<object>()))
                                   .Returns(Task.CompletedTask)
                                   .Verifiable();

        //// Act
        var result = await _handler.Handle(new DeleteGrantFundingSignatoryRequest { GrantFundingSignatoryId = Guid.NewGuid() }, CancellationToken.None);

        _connection.Verify();
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(),
                                               It.IsAny<object>()),
                                               Times.Once);
    }
}
