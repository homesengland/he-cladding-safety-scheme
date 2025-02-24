using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.SetAppraisalSurveyDetails;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWallWorks;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class SetDeleteExternalWallWorksTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IFireRiskWorksRepository> _fireRiskWorksRepository;

    private readonly SetDeleteExternalWallWorksHandler _handler;
        
    public SetDeleteExternalWallWorksTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);        
        _fireRiskWorksRepository = new Mock<IFireRiskWorksRepository>(MockBehavior.Strict);

        _handler = new SetDeleteExternalWallWorksHandler(_applicationDataProvider.Object,
                                                         _connection.Object,                                                        
                                                         _fireRiskWorksRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_Delete_External_Wall_Works()
    {
        //Arrange                
        _fireRiskWorksRepository.Setup(x => x.DeleteFireRiskWallWorks(It.IsAny<Guid>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 
        
        //// Act
        var result = await _handler.Handle(new SetDeleteExternalWallWorksRequest 
        { 
            Id = Guid.NewGuid()
        }, CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetUserId(), Times.Once);        
    }

    [Fact]
    public async Task Handler_Sets_Delete_External_Wall_Works_Handles_Null()
    {
        //Arrange                
        _fireRiskWorksRepository.Setup(x => x.DeleteFireRiskWallWorks(It.IsAny<Guid>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();
        
        _applicationDataProvider.Setup(x => x.GetUserId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 
        
        //// Act
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(new SetDeleteExternalWallWorksRequest(), CancellationToken.None));
    }
}
