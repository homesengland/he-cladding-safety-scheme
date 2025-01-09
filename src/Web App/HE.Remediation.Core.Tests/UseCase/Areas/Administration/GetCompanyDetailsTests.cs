using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyDetails.GetCompanyDetailsForCurrentUser;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Administration;

public class GetCompanyDetailsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly GetCompanyDetailsForCurrentUserHandler _handler;

    public GetCompanyDetailsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetCompanyDetailsForCurrentUserHandler(_applicationDataProvider.Object, _connection.Object);
    }

    [Fact]
    public async Task Handler_Returns_User_Selection_From_DB()
    {
        //Arrange
        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetCompanyDetailsForCurrentUserResponse>("GetCompanyDetailsByUserId", 
                                                                                                    It.IsAny<object>()))
            .ReturnsAsync(() => new GetCompanyDetailsForCurrentUserResponse
            {
                CompanyName = "New Company",
                CompanyRegistrationNumber = "12345678",
                UserRoleInCompany = "Manager"
            })
            .Verifiable();
            
        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        //// Act
        var result = await _handler.Handle(GetCompanyDetailsForCurrentUserRequest.Request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);        
        _connection.Verify();
        _applicationDataProvider.Verify();
    }

   
}
