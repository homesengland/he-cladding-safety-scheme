using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.GetBuildingUniqueName;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.SetBuildingUniqueName;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class SetBuildingUniqueNameTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IBuildingDetailsRepository> _buildingDetailsRepository;
    

    private readonly SetBuildingUniqueNameHandler _handler;
        
    public SetBuildingUniqueNameTests()
    {       
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _buildingDetailsRepository = new Mock<IBuildingDetailsRepository>(MockBehavior.Strict);        

        _handler = new SetBuildingUniqueNameHandler(_connection.Object,
                                                    _applicationDataProvider.Object,
                                                    _buildingDetailsRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_Building_Unique_Name_Update()
    {
        //Arrange                        
        string buildingName = "my building";
        _buildingDetailsRepository.Setup(x => x.GetBuildingUniqueName(It.IsAny<Guid>()))
                                .ReturnsAsync(buildingName)
                                .Verifiable();
        
        _connection.Setup(x => x.ExecuteAsync("UpdateBuildingUniqueName", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();
                
        //// Act
        var result = await _handler.Handle(new SetBuildingUniqueNameRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Once);
    }

    [Fact]
    public async Task Handler_Sets_Building_Unique_Name_Insert()
    {
        //Arrange                
        _connection.Setup(x => x.ExecuteAsync("InsertBuildingUniqueName", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _connection.Setup(x => x.ExecuteAsync("UpdateBuildingDetailsId", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        string buildingName = null; // "my building";
        _buildingDetailsRepository.Setup(x => x.GetBuildingUniqueName(It.IsAny<Guid>()))
                                .ReturnsAsync(buildingName)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(new SetBuildingUniqueNameRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Exactly(2));
    }
}
