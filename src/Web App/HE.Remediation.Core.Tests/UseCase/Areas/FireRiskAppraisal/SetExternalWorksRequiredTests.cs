using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.SetCompanyAddressForCurrentUser;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWorksRequired;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class SetExternalWorksRequiredTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IFireRiskWorksRepository> _fireRiskWorksRepository;
    private readonly SetExternalWorksRequiredHandler _handler;
        
    public SetExternalWorksRequiredTests()
    {       
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _fireRiskWorksRepository = new Mock<IFireRiskWorksRepository>(MockBehavior.Strict);        
        _handler = new SetExternalWorksRequiredHandler(_applicationDataProvider.Object, _fireRiskWorksRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_External_Works_Required_Yes()
    {
        //Arrange                
        _fireRiskWorksRepository.Setup(x => x.SetExternalWorksRequired(It.IsAny<Guid>(), It.IsAny<ENoYes>()))
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        ////// Act
        var result = await _handler.Handle(new SetExternalWorksRequiredRequest { WorkRequired = ENoYes.Yes }, CancellationToken.None);

        //// Assert
        _fireRiskWorksRepository.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
    }    


        [Fact]
    public async Task Handler_Sets_External_Works_Required_No()
    {
        //Arrange                
        _fireRiskWorksRepository.Setup(x => x.SetExternalWorksRequired(It.IsAny<Guid>(), It.IsAny<ENoYes>()))
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _fireRiskWorksRepository.Setup(x => x.ResetFireRiskWallWorks(It.IsAny<Guid>(), It.IsAny<EWorkType>()))
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        ////// Act        
        var result = await _handler.Handle(new SetExternalWorksRequiredRequest { WorkRequired = ENoYes.No }, CancellationToken.None);
        
        //// Assert
        _fireRiskWorksRepository.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
    }    
}
