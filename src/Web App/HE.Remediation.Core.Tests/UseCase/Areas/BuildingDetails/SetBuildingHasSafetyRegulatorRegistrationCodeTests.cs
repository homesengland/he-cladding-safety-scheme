using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingHasSafetyRegulatorRegistrationCode.SetBuildingHasSafetyRegulatorRegistrationCode;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.BuildingDetails;

public class SetBuildingHasSafetyRegulatorRegistrationCodeTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly SetBuildingHasSafetyRegulatorRegistrationCodeHandler _handler;

    public SetBuildingHasSafetyRegulatorRegistrationCodeTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new SetBuildingHasSafetyRegulatorRegistrationCodeHandler(_connection.Object, 
                                                    _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Sets_Building_Has_Safety_Regulator_Registration_Code()
    {
        //Arrange                        
        _connection.Setup(x => x.ExecuteAsync("UpdateBuildingHasSafetyRegulatorRegistrationCode", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();
                
        //// Act
        var result = await _handler.Handle(new SetBuildingHasSafetyRegulatorRegistrationCodeRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Once);
    }
}
