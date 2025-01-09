using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddExternalWallWorks;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class SetAddExternalWallWorksTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IFireRiskWorksRepository> _fireRiskWorksRepository;

    private readonly SetAddExternalWallWorksHandler _handler;
        
    public SetAddExternalWallWorksTests()
    {       
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _fireRiskWorksRepository = new Mock<IFireRiskWorksRepository>(MockBehavior.Strict);

        _handler = new SetAddExternalWallWorksHandler(_applicationDataProvider.Object, _fireRiskWorksRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_Add_External_Wall_Works_Insert()
    {
        //Arrange        
        _fireRiskWorksRepository.Setup(x => x.InsertWallWorks(It.IsAny<EWorkType>(), 
                                                              It.IsAny<string>(), 
                                                              It.IsAny<Guid>()))
                                .Returns(Task.FromResult(true));        

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 
                
        //// Act
        var result = await _handler.Handle(new SetAddExternalWallWorksRequest(), CancellationToken.None);

        // Assert
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _fireRiskWorksRepository.Verify(x => x.InsertWallWorks(It.IsAny<EWorkType>(), It.IsAny<string>(), It.IsAny<Guid>()), 
                                               Times.Once);
    }

    [Fact]
    public async Task Handler_Sets_Add_External_Wall_Works_Update()
    {
        //Arrange        
        _fireRiskWorksRepository.Setup(x => x.UpdateWallWorks(It.IsAny<EWorkType>(), 
                                                              It.IsAny<string>(), 
                                                              It.IsAny<Guid>(), 
                                                              It.IsAny<Guid>()))
                                .Returns(Task.FromResult(true));        

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(new SetAddExternalWallWorksRequest
        {
            Id = Guid.NewGuid(),
            Description = "testing"
        }, CancellationToken.None);

        // Assert
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _fireRiskWorksRepository.Verify(x => x.UpdateWallWorks(It.IsAny<EWorkType>(), 
                                                               It.IsAny<string>(), 
                                                               It.IsAny<Guid>(), 
                                                               It.IsAny<Guid>()), 
                                                               Times.Once);
    }
}
