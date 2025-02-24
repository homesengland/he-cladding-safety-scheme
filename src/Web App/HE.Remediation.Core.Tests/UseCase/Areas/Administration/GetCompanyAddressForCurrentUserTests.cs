using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.GetCompanyAddressForCurrentUser;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Administration;

public class GetCompanyAddressForCurrentUserTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly GetCompanyAddressForCurrentUserHandler _handler;
    
    public GetCompanyAddressForCurrentUserTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetCompanyAddressForCurrentUserHandler(_applicationDataProvider.Object, _connection.Object);
    }

    [Fact]
    public async Task Handler_Returns_User_Selection_From_DB()
    {
        //Arrange
        var response = new GetCompanyAddressForCurrentUserResponse()
        {  
            NameNumber = "User 1",
            AddressLine1 = "10 bus lane",
            AddressLine2 = "64 bit",
            City = "London",
            County = "Greater London",
            Postcode = "W12 8QT"
        };

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetCompanyAddressForCurrentUserResponse>("GetCompanyAddressByUserId", It.IsAny<object>()))
                                .ReturnsAsync(response)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetCompanyAddressForCurrentUserRequest.Request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.City != "London");
        _connection.Verify();
        _applicationDataProvider.Verify();
    }

    [Fact]
    public async Task Handler_Returns_Null_With_No_DB_Results()
    {
        //Arrange
        GetCompanyAddressForCurrentUserResponse response = null;

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetCompanyAddressForCurrentUserResponse>("GetCompanyAddressByUserId", It.IsAny<object>()))
                                .ReturnsAsync(response)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetCompanyAddressForCurrentUserRequest.Request, CancellationToken.None);

        // Assert
        Assert.Null(result);        
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
