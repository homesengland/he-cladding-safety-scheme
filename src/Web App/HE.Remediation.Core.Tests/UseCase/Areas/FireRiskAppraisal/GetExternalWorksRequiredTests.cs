using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWorksRequired;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class GetExternalWorksRequiredTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IFireRiskWorksRepository> _fireRiskWorksRepository;

    private readonly GetExternalWorksRequiredHandler _handler;
    
    public GetExternalWorksRequiredTests()
    {
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _fireRiskWorksRepository = new Mock<IFireRiskWorksRepository>(MockBehavior.Strict);
        _handler = new GetExternalWorksRequiredHandler(_fireRiskWorksRepository.Object, _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange                
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _fireRiskWorksRepository.Setup(x => x.GetWorksRequired(It.IsAny<Guid>()))
                                .ReturnsAsync(() => new FireRiskWorksRequiredResult 
                                { 
                                    ExternalWorksRequired = ENoYes.Yes
                                })                                
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(new GetExternalWorksRequiredRequest(), CancellationToken.None);

        // Assert        
        Assert.NotNull(result);        
        Assert.True(result.WorksRequired.Value == ENoYes.Yes);
        _applicationDataProvider.Verify();
    }
}
