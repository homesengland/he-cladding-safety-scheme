using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.Location;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.SetBuildingAddressManual;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class SetBuildingAddressManualTests
{    
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IPostCodeLookup> _postCodeLookup;
    private readonly Mock<IBuildingDetailsRepository> _buildingDetailsRepo;

    private readonly SetBuildingAddressManualHandler _handler;

    public SetBuildingAddressManualTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _postCodeLookup = new Mock<IPostCodeLookup>(MockBehavior.Strict);
        _buildingDetailsRepo = new Mock<IBuildingDetailsRepository>(MockBehavior.Strict);        
        
        _handler = new SetBuildingAddressManualHandler(_connection.Object,
                                                       _applicationDataProvider.Object,
                                                       _postCodeLookup.Object,
                                                       _buildingDetailsRepo.Object);
    }

    [Fact]
    public async Task Handler_Sets_Building_Address_Manual_With_Update()
    {
        //Arrange                        
      var addressResponse = new GetBuildingAddressResponse()
        {
            NonResidentialUnits = true,
            NameNumber = "10",
            AddressLine1 = "10 High Road",
            AddressLine2 = null,
            City = "Chelmsford",   
            County = "Essex",
            Postcode = "CM1 2RT"
        };
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 
        
        _buildingDetailsRepo.Setup(x => x.GetBuildingAddress(It.IsAny<Guid>()))
                            .ReturnsAsync(addressResponse)
                            .Verifiable();

        _buildingDetailsRepo.Setup(x => x.UpdateBuildingAddress(It.IsAny<BuildingDetailsAddressDetails>(), It.IsAny<Guid>()))
                            .Returns(Task.CompletedTask)
                            .Verifiable();
                
        ////// Act
        var result = await _handler.Handle(new SetBuildingAddressManualRequest 
        { 
            NameNumber = "10",
            AddressLine1 = "10 High Road",
            AddressLine2 = "Teg Down",
            City = "Chelmsford",
            LocalAuthority = null,
            County = "Essex",
            Postcode = "CM1 2RT"
        }, CancellationToken.None);

        // Assert
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _buildingDetailsRepo.Verify(x => x.GetBuildingAddress(It.IsAny<Guid>()),
                                                              Times.Once);
        _buildingDetailsRepo.Verify(x => x.UpdateBuildingAddress(It.IsAny<BuildingDetailsAddressDetails>(), 
                                                                 It.IsAny<Guid>()),
                                                                 Times.Once);
    }

    [Fact]
    public async Task Handler_Sets_Building_Address_Manual_With_Insert()
    {
        //Arrange                        
        GetBuildingAddressResponse addressResponse = null;
        
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 
        
        _buildingDetailsRepo.Setup(x => x.GetBuildingAddress(It.IsAny<Guid>()))
                            .ReturnsAsync(addressResponse)
                            .Verifiable();

        _buildingDetailsRepo.Setup(x => x.InsertBuildingAddress(It.IsAny<BuildingDetailsAddressDetails>(), It.IsAny<Guid>()))
                            .Returns(Task.CompletedTask)
                            .Verifiable();
                
        ////// Act
        var result = await _handler.Handle(new SetBuildingAddressManualRequest 
        { 
            NameNumber = "10",
            AddressLine1 = "10 High Road",
            AddressLine2 = "Teg Down",
            City = "Chelmsford",
            LocalAuthority = null,
            County = "Essex",
            Postcode = "CM1 2RT"
        }, CancellationToken.None);

        // Assert
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);        
        _buildingDetailsRepo.Verify(x => x.GetBuildingAddress(It.IsAny<Guid>()),
                                                              Times.Once);
        _buildingDetailsRepo.Verify(x => x.InsertBuildingAddress(It.IsAny<BuildingDetailsAddressDetails>(), 
                                                                 It.IsAny<Guid>()),
                                                                 Times.Once);
    }
}
