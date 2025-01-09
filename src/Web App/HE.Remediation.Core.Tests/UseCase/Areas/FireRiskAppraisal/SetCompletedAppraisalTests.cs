using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CompletedAppraisal.SetCompletedAppraisal;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class SetCompletedAppraisalTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly SetCompletedAppraisalHandler _handler;
        
    public SetCompletedAppraisalTests()
    {       
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);

        _handler = new SetCompletedAppraisalHandler(_applicationDataProvider.Object, _connection.Object);
    }

    [Fact]
    public async Task Handler_Sets_Appraisal()
    {
        //Arrange        
        _connection.Setup(x => x.ExecuteAsync("InsertOrUpdateFireRiskCompletedStatus", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 
                
        //// Act
        var result = await _handler.Handle(new SetCompletedAppraisalRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Once);
    }
}
