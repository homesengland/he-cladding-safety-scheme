using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.InviteMember;
using Microsoft.Data.SqlClient;
using Moq;
using System.Data.Common;

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

    [Theory]
    [InlineData("Invalid admin user.", typeof(InvalidAdminOrganisationException))]
    [InlineData("User with given email already exists.", typeof(UserEmailExistsException))]
    [InlineData("Max number of admins exceeded.", typeof(MaximumAdminsException))]
    [InlineData("Already associated with an organisation.", typeof(OrganisationAssociationException))]
    public async Task Handler_Returns_Error_From_DB_For_Insert(string sqlExceptionMessage, Type expectedExceptionType)
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<Guid>("InsertProvisionalCollaborationUser", It.IsAny<object>()))
            .ThrowsAsync(CreateSqlException(sqlExceptionMessage));

        var insertRequest = new UpsertMemberRequest() { CollaborationUserId = null };

        // Act / Assert
        await Assert.ThrowsAsync(expectedExceptionType, () => _handler.Handle(insertRequest, CancellationToken.None));
    }

    // Helper method to create a SqlException instance for testing (no public constructor)
    private static SqlException CreateSqlException(string message)
    {
        var errorCollection = (SqlErrorCollection)Activator.CreateInstance(
            typeof(SqlErrorCollection), true);

        var constructors = typeof(SqlError).GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

        var error = (SqlError)constructors
            .First(ctor => ctor.GetParameters().Length == 8) // 8 parameters expected in .NET 8
            .Invoke([0, (byte)0, (byte)0, "", "", message, 0, new Exception()]);

        typeof(SqlErrorCollection)
            .GetMethod("Add", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            .Invoke(errorCollection, [error]);

        var exception = (SqlException)typeof(SqlException)
            .GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)[0]
            .Invoke([message, errorCollection, null, Guid.NewGuid()]);

        return exception;
    }
}
