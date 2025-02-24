using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyDetails.GetCompanyDetailsForCurrentUser;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyDetails.SetCompanyDetailsForCurrentUser;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.GetCorrespondenceAddress;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Administration;

public class GetCorrespondenceAddressTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly GetCorrespondenceAddressHandler _handler;

    public GetCorrespondenceAddressTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetCorrespondenceAddressHandler(_applicationDataProvider.Object, _connection.Object);
    }

    [Fact]
    public async Task Handler_Returns_User_Selection_From_DB()
    {
        //Arrange
        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetCorrespondenceAddressResponse>("GetCorrespondanceAddressDetails", 
                                                                                                    It.IsAny<object>()))
            .ReturnsAsync(() => new GetCorrespondenceAddressResponse
            {
                NameNumber = "5",
                AddressLine1 = "Bradley Peak",
                AddressLine2 = "Teg Down",
                City = "Winchester",
                County = "Hampshire",
                Postcode = "SO22 5NL"
            })
            .Verifiable();
            
        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        //// Act
        var result = await _handler.Handle(GetCorrespondenceAddressRequest.Request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);        
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
