using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.InviteMember;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.OrganisationManagement;

public class UpsertMemberHandlerTests
{
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly UpsertMemberHandler _handler;

    public UpsertMemberHandlerTests()
    {
        _connection = new Mock<IDbConnectionWrapper>();
        _handler = new UpsertMemberHandler(_connection.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB_For_Insert()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<Guid>("InsertProvisionalCollaborationUser", It.IsAny<object>()))
                                .ReturnsAsync(expectedUserId)
                                .Verifiable();

        var insertRequest = new UpsertMemberRequest() { CollaborationUserId = null };

        // Act
        var result = await _handler.Handle(insertRequest, CancellationToken.None);

        // Assert        
        Assert.Equal(expectedUserId, result.CollaborationUserId);
        _connection.Verify();
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB_For_Update()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();

        _connection.Setup(x => x.ExecuteAsync("UpdateCollaborationUser", It.IsAny<object>()))
                                .Verifiable();

        var updateRequest = new UpsertMemberRequest() { CollaborationUserId = expectedUserId };

        // Act
        var result = await _handler.Handle(updateRequest, CancellationToken.None);

        // Assert        
        Assert.Equal(expectedUserId, result.CollaborationUserId);
        _connection.Verify();
    }
}
