using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.Users;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.OrganisationManagement;

public class UsersHandlerTests
{
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly UsersHandler _handler;

    public UsersHandlerTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _handler = new UsersHandler(_connection.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        // Arrange
        var stubbedResult = new List<UsersResponse.CollaborationUser>()
        {
            new(Guid.NewGuid(), "Bruce Wayne", "batman@dc.com", 1, 0, "auth0|12345")
        };
        _connection.Setup(x => x.QueryAsync<UsersResponse.CollaborationUser>("GetCollaborationOrganisationUsers", It.IsAny<object>()))
                                .ReturnsAsync(stubbedResult)
                                .Verifiable();

        // Act
        var result = await _handler.Handle(new UsersRequest("auth0|id"), CancellationToken.None);

        // Assert        
        Assert.NotNull(result);
        Assert.Single(result.Users);
        Assert.Equal("Bruce Wayne", result.Users.First().Name);
        _connection.Verify();
    }
}
