using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Administration.AdditionalContacts.GetAdditionalContact;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Administration;

public class GetAdditionalContactTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly GetAdditionalContactHandler _handler;
    
    public GetAdditionalContactTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetAdditionalContactHandler(_applicationDataProvider.Object, _connection.Object);
    }

    [Fact]
    public async Task Handler_Returns_User_Selection_From_DB()
    {
        //Arrange
        Guid firstGuid = Guid.NewGuid();
        Guid secondGuid = Guid.NewGuid();
        _connection.Setup(x => x.QueryAsync<GetAdditionalContactResponse>("GetUserSecondaryContactDetails", It.IsAny<object>()))
            .ReturnsAsync(() => new[]
            {
                new GetAdditionalContactResponse
                {
                    Id = firstGuid,
                    Name = "First contact",
                    ContactNumber = "123456",
                    EmailAddress = "test1@test.com"
                },
                new GetAdditionalContactResponse
                {
                    Id = secondGuid,
                    Name = "Second contact",
                    ContactNumber = "789012",
                    EmailAddress = "test2@test.com"
                }
            })
            .Verifiable();
            
        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        //// Act
        var result = await _handler.Handle(GetAdditionalContactRequest.Request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.FirstOrDefault()?.Id != firstGuid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }

    [Fact]
    public async Task Handler_Returns_Null_With_No_Details_From_DB()
    {
        //Arrange                
        _connection.Setup(x => x.QueryAsync<GetAdditionalContactResponse>("GetUserSecondaryContactDetails", It.IsAny<object>()))
            .ReturnsAsync(() => null)
            .Verifiable();
            
        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        //// Act
        var result = await _handler.Handle(GetAdditionalContactRequest.Request, CancellationToken.None);

        // Assert
        Assert.Null(result);        
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
