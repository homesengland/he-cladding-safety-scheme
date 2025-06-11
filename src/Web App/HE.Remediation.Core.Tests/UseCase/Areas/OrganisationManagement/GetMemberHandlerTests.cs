using AutoFixture;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.InviteMember;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.OrganisationManagement;

public class GetMemberHandlerTests
{
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetMemberHandler _handler;
    private readonly Fixture _fixture;

    public GetMemberHandlerTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _handler = new GetMemberHandler(_connection.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        // Arrange
        var dbResponse = _fixture.Create<GetMemberResult>();
        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetMemberResult>("GetCollaborationOrganisationUser", It.IsAny<object>()))
                                .ReturnsAsync(dbResponse)
                                .Verifiable();

        // Act
        var result = await _handler.Handle(new GetMemberRequest(), CancellationToken.None);

        // Assert        
        Assert.Equal(dbResponse.FirstName, result.FirstName);
        Assert.Equal(dbResponse.LastName, result.LastName);
        Assert.Equal(dbResponse.Id, result.CollaborationUserId);
        Assert.Equal((EApplicationRole)dbResponse.ApplicationRoleId, result.ApplicationRole);
        Assert.Equal(dbResponse.CollaborationOrganisationId, result.OrganisationId);
        Assert.Equal(dbResponse.EmailAddress, result.Email);
        Assert.Equal((ECollaborationUserStatus)dbResponse.UserStatusId, result.UserStatus);
        _connection.Verify();
    }
}
