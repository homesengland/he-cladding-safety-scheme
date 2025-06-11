using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.OrganisationDetails;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.OrganisationManagement;

public class OrganisationDetailsHandlerTests
{
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly OrganisationDetailsHandler _handler;

    public OrganisationDetailsHandlerTests()
    {
        _connection = new Mock<IDbConnectionWrapper>();
        _handler = new OrganisationDetailsHandler(_connection.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        // Arrange
        var request = new OrganisationDetailsRequest()
        {
            Name = "Wayne Enterprises",
            RegistrationNumber = "123456789",
            UserId = "auth0|id"
        };

        _connection.Setup(x => x.ExecuteAsync("InsertCollaborationOrganisation", It.IsAny<OrganisationDetailsRequest>())).Verifiable();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert        
        Assert.Equal(request.Name, result.Name);
        _connection.Verify();
    }
}
