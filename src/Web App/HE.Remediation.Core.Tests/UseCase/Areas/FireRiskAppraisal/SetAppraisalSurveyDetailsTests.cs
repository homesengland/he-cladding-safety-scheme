using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.SetAppraisalSurveyDetails;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class SetAppraisalSurveyDetailsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IStatusTransitionService> _statusTransitionService;

    private readonly SetAppraisalSurveyDetailsHandler _handler;
        
    public SetAppraisalSurveyDetailsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _statusTransitionService = new Mock<IStatusTransitionService>();

        _handler = new SetAppraisalSurveyDetailsHandler(_connection.Object,
                                                        _applicationDataProvider.Object,
                                                        _statusTransitionService.Object);
    }

    [Fact]
    public async Task Handler_Sets_Assessor_Details_DB()
    {
        //Arrange                
        _connection.Setup(x => x.ExecuteAsync("InsertOrUpdateAppraisalSurveyDetails", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _statusTransitionService.Setup(x => x.TransitionToInternalStatus(It.IsAny<EApplicationInternalStatus>(), null, It.IsAny<Guid[]>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        //// Act
        var result = await _handler.Handle(new SetAppraisalSurveyDetailsRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);        
    }    
}
