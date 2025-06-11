using AutoFixture;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Communication.Collaboration;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.InvitationDeclaration;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.OrganisationManagement;

public class InvitationDeclarationHandlerTests
{
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly InvitationDeclarationHandler _handler;
    private readonly Fixture _fixture;

    public InvitationDeclarationHandlerTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        var queue = new Mock<IBackgroundCollaborationCommunicationQueue>();
        _handler = new InvitationDeclarationHandler(_connection.Object, queue.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        // Arrange
        var stubbedResult = _fixture.Create<InvitationDeclarationResponse>();
        _connection.Setup(x => x.QuerySingleOrDefaultAsync<InvitationDeclarationResponse>("GetCollaborationUserInviteDetails", It.IsAny<object>()))
                                .ReturnsAsync(stubbedResult)
                                .Verifiable();

        // Act
        var result = await _handler.Handle(new InvitationDeclarationRequest(), CancellationToken.None);

        // Assert        
        Assert.NotNull(result.OrganisationName);
        Assert.NotNull(result.InvitationTo);
        Assert.NotNull(result.InvitationEmailAddress);
        Assert.NotNull(result.RequestorFullName);
        Assert.NotEqual(Guid.Empty, result.CollaborationOrganisationUserId);
        _connection.Verify();
    }
}
