using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorDetails.SetAssessorDetails;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class SetAccessorDetailsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    
    private readonly SetAssessorDetailsHandler _handler;
        
    public SetAccessorDetailsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);        
        _handler = new SetAssessorDetailsHandler(_applicationDataProvider.Object, _connection.Object);
    }

    [Fact]
    public async Task Handler_Sets_Assessor_Details_DB()
    {
        //Arrange                
        _connection.Setup(x => x.ExecuteAsync("InsertOrUpdateFireRiskAssessmentAssessorDetails", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 
        
        //// Act
        var result = await _handler.Handle(new SetAssessorDetailsRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);        
    }    
}
