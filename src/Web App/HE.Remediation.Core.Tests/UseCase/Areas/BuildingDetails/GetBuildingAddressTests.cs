using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class GetBuildingAddressTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetBuildingAddressHandler _handler;

    public GetBuildingAddressTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new GetBuildingAddressHandler(_connection.Object, 
                                                     _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        var addressResponse = new GetBuildingAddressResponse()
        {
            NonResidentialUnits = true,
            NameNumber = "12",
            AddressLine1 = "10 High Road",
            AddressLine2 = null,
            City = "Chelmsford",   
            County = "Essex",
            Postcode = "CM1 2RT"
        };
        
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetBuildingAddressResponse>("GetBuildingAddress", It.IsAny<object>()))
                                .ReturnsAsync(addressResponse)
        .Verifiable();

        //// Act
        var result = await _handler.Handle(GetBuildingAddressRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.NonResidentialUnits.Value == true));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
