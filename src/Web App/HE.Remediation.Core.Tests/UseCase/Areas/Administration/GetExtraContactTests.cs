using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Administration.AddExtraContact.GetExtraContact;
using HE.Remediation.Core.Enums;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Administration;

public class GetExtraContactTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly GetExtraContactHandler _handler;
        
    public GetExtraContactTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetExtraContactHandler(_applicationDataProvider.Object, _connection.Object);
    }

    [Fact]
    public async Task Handler_Returns_User_Selection_From_DB()
    {
        //Arrange
        var response = new GetExtraContactResponse()
        {  
            AddContact = ENoYes.Yes
        };

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetExtraContactResponse>("GetUserAddSecondaryContactState", It.IsAny<object>()))
                                .ReturnsAsync(response)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); ;

        //// Act
        var result = await _handler.Handle(GetExtraContactRequest.Request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.AddContact != ENoYes.Yes);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }

    [Fact]
    public async Task Handler_Returns_Null_User_Selection_From_DB()
    {
        //Arrange
        var response = new GetExtraContactResponse();

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetExtraContactResponse>("GetUserAddSecondaryContactState", It.IsAny<object>()))
                                .ReturnsAsync(response)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); ;

        //// Act
        var result = await _handler.Handle(GetExtraContactRequest.Request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.AddContact);
    }
}
