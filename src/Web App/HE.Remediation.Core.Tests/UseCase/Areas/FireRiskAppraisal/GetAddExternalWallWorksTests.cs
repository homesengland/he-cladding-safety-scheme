using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AddExternalWallWorks;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class GetAddExternalWallWorksTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IFireRiskWorksRepository> _fireRiskWorksRepo;

    private readonly GetAddExternalWallWorksHandler _handler;
        
    public GetAddExternalWallWorksTests()
    {
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _fireRiskWorksRepo = new Mock<IFireRiskWorksRepository>(MockBehavior.Strict);

        _handler = new GetAddExternalWallWorksHandler(_fireRiskWorksRepo.Object, 
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

        _fireRiskWorksRepo.Setup(x => x.GetFireRiskWallWorksDetail(It.IsAny<Guid>()))
                                .ReturnsAsync(workDetails)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(new GetAddExternalWallWorksRequest 
        { 
            Id = Guid.NewGuid()
        }, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.Description == "testing"));
        Assert.True(resultValid);

        _fireRiskWorksRepo.Verify();
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

        _fireRiskWorksRepo.Setup(x => x.GetFireRiskWallWorksDetail(It.IsAny<Guid>()))
                                .ReturnsAsync(workDetails)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(new GetAddExternalWallWorksRequest(), CancellationToken.None);

        // Assert        
        Assert.Null(result);        
        _fireRiskWorksRepo.Verify(x => x.GetFireRiskWallWorksDetail(It.IsAny<Guid>()),                                                
                                               Times.Never);
    }
}
