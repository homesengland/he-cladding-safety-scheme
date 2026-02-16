using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddress;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class SetBuildingAddressTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IBuildingDetailsRepository> _buildingDetailsRepo;

    private readonly SetBuildingAddressHandler _handler;

    public SetBuildingAddressTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _buildingDetailsRepo = new Mock<IBuildingDetailsRepository>(MockBehavior.Strict);
        
        _handler = new SetBuildingAddressHandler(_connection.Object, 
                                                _applicationDataProvider.Object,
                                                _buildingDetailsRepo.Object);
    }

    [Fact]
    public async Task Handler_Sets_Building_Address_With_Update()
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

        _applicationDataProvider.Setup(x => x.GetApplicationId())
        .Returns(Guid.NewGuid())
        .Verifiable();

        _buildingDetailsRepo.Setup(x => x.UpdateBuildingAddress(It.IsAny<BuildingDetailsAddressDetails>(), It.IsAny<Guid>()))
                            .Returns(Task.CompletedTask)
                            .Verifiable();
                
        ////// Act
        var result = await _handler.Handle(new SetBuildingAddressRequest 
        { 
            SelectedAddressId = "{\"Address\":\"3, BRADLEY PEAK, WINCHESTER, SO22 5NL\",\"BuildingNumber\":\"3\",\"Locality\":null,\"Street\":\"BRADLEY PEAK\",\"Town\":\"WINCHESTER\",\"SubBuildingName\":null,\"BuildingName\":null,\"Postcode\":\"SO22 5NL\",\"Organisation\":null}"
        }, CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.QuerySingleOrDefaultAsync<GetBuildingAddressResponse>(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Once);
        _buildingDetailsRepo.Verify(x => x.UpdateBuildingAddress(It.IsAny<BuildingDetailsAddressDetails>(), 
                                                                 It.IsAny<Guid>()),
                                                                 Times.Once);
    }

    [Fact]
    public async Task Handler_Sets_Building_Address_With_Insert()
    {
        //Arrange                        
        GetBuildingAddressResponse? response = null;
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetBuildingAddressResponse?>("GetBuildingAddress", It.IsAny<object>()))
                                .ReturnsAsync(response)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
        .Returns(Guid.NewGuid())
        .Verifiable();

        _buildingDetailsRepo.Setup(x => x.InsertBuildingAddress(It.IsAny<BuildingDetailsAddressDetails>(), It.IsAny<Guid>()))
                            .Returns(Task.CompletedTask)
                            .Verifiable();

                
        ////// Act
        var result = await _handler.Handle(new SetBuildingAddressRequest 
        { 
            SelectedAddressId = "{\"Address\":\"3, BRADLEY PEAK, WINCHESTER, SO22 5NL\",\"BuildingNumber\":\"3\",\"Locality\":null,\"Street\":\"BRADLEY PEAK\",\"Town\":\"WINCHESTER\",\"SubBuildingName\":null,\"BuildingName\":null,\"Postcode\":\"SO22 5NL\",\"Organisation\":null}"
        }, CancellationToken.None);


        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.QuerySingleOrDefaultAsync<GetBuildingAddressResponse>(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Once);
    }
}
