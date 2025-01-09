using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWallWorks;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class GetExternalWallWorksTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IFireRiskWorksRepository> _fireRiskWorksRepository;

    private readonly GetExternalWallWorksHandler _handler;
    
    public GetExternalWallWorksTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _fireRiskWorksRepository = new Mock<IFireRiskWorksRepository>(MockBehavior.Strict);
        _handler = new GetExternalWallWorksHandler(_connection.Object, _applicationDataProvider.Object, 
                                                   _fireRiskWorksRepository.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _fireRiskWorksRepository.Setup(x => x.GetFireRiskWallWorks(It.IsAny<Guid>(), It.IsAny<Enums.EWorkType>()))
                                .ReturnsAsync(() => new List<GetWallWorksListResult>())
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetExternalWallWorksRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
