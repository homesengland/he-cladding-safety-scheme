using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ProvideBuildingAddress.GetBuildingAddress;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddExternalWallWorks;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddInternalWallWorks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class GetAddInternalWallWorksTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IFireRiskWorksRepository> _fireRiskWorksRepository;

    private readonly GetAddInternalWallWorksHandler _handler;

    public GetAddInternalWallWorksTests()
    {
        _fireRiskWorksRepository = new Mock<IFireRiskWorksRepository>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new GetAddInternalWallWorksHandler(_fireRiskWorksRepository.Object, 
                                                      _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange                
        var workDetails = new GetWallWorksListResult()
        {
            Id = Guid.NewGuid(),
            Description ="testing"
        };

        _fireRiskWorksRepository.Setup(x => x.GetFireRiskWallWorksDetail(It.IsAny<Guid>()))
                                .ReturnsAsync(workDetails)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(new GetAddInternalWallWorksRequest 
        { 
            Id = Guid.NewGuid()
        }, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.Description == "testing"));
        Assert.True(resultValid);

        _fireRiskWorksRepository.Verify();
    }

    [Fact]
    public async Task Handler_Handles_Null_ID()
    {
        //Arrange                
        var workDetails = new GetWallWorksListResult()
        {
            Id = Guid.NewGuid(),
            Description ="testing"
        };

        _fireRiskWorksRepository.Setup(x => x.GetFireRiskWallWorksDetail(It.IsAny<Guid>()))
                                .ReturnsAsync(workDetails)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(new GetAddInternalWallWorksRequest(), CancellationToken.None);

        // Assert        
        Assert.Null(result);        
        _fireRiskWorksRepository.Verify(x => x.GetFireRiskWallWorksDetail(It.IsAny<Guid>()),                                                
                                               Times.Never);
    }
}
