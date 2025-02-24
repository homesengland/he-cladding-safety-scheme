using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Administration.Dashboard.GetProfile;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Administration;

public class GetProfileTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly GetProfileHandler _handler;
    
    public GetProfileTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetProfileHandler(_applicationDataProvider.Object, _connection.Object);
    }

    [Fact]
    public async Task Handler_Returns_User_Selection_From_DB()
    {
        //Arrange
        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetProfileResponse>("GetUserResponsibleEntityTypeByUserId", It.IsAny<object>()))
            .ReturnsAsync(() => new GetProfileResponse
            {
                ResponsibleEntityTypeId = EResponsibleEntityType.Individual
            })
            .Verifiable();

        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetProfileRequest.Request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
