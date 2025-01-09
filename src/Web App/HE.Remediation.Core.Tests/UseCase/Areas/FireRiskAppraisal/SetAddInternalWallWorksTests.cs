using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Declaration.SetConfirmDeclaration;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddInternalWallWorks;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWorksRequired;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class SetAddInternalWallWorksTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IFireRiskWorksRepository> _fireRiskWorksRepository;

    private readonly SetAddInternalWallWorksHandler _handler;

    public SetAddInternalWallWorksTests()
    {
        _fireRiskWorksRepository = new Mock<IFireRiskWorksRepository>(MockBehavior.Strict);        
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        
        _handler = new SetAddInternalWallWorksHandler(_applicationDataProvider.Object,
                                                      _fireRiskWorksRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_Add_Internal_Wall_Works_Insert()
    {
        //Arrange                
        _fireRiskWorksRepository.Setup(x => x.InsertWallWorks(It.IsAny<EWorkType>(), 
                                                              It.IsAny<String>(), 
                                                              It.IsAny<Guid>()))
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        ////// Act
        var result = await _handler.Handle(new SetAddInternalWallWorksRequest(), CancellationToken.None);

        //// Assert
        _fireRiskWorksRepository.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
    }

    [Fact]
    public async Task Handler_Sets_Add_Internal_Wall_Works_Update()
    {
        //Arrange                
        _fireRiskWorksRepository.Setup(x => x.UpdateWallWorks(It.IsAny<EWorkType>(), 
                                                              It.IsAny<String>(), 
                                                              It.IsAny<Guid>(),
                                                              It.IsAny<Guid>()))
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        ////// Act
        var result = await _handler.Handle(new SetAddInternalWallWorksRequest 
        { 
            Id = Guid.NewGuid()
        }, CancellationToken.None);

        //// Assert
        _fireRiskWorksRepository.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
    }
}
