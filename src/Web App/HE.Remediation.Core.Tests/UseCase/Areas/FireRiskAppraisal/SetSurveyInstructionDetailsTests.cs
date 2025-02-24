using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.SetSurveyInstructionDetails;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class SetSurveyInstructionDetailsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IStatusTransitionService> _statusTransitionService;

    private readonly SetSurveyInstructionDetailsHandler _handler;
        
    public SetSurveyInstructionDetailsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _statusTransitionService = new Mock<IStatusTransitionService>();

        _handler = new SetSurveyInstructionDetailsHandler(
            _connection.Object, 
            _applicationDataProvider.Object, 
            _statusTransitionService.Object);
    }

    [Fact]
    public async Task Handler_Sets_Survey_Instruction_Details()
    {
        //Arrange                
        _connection.Setup(x => x.ExecuteAsync("InsertOrUpdateSurveyInstructionDetails", It.IsAny<object>()))
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _statusTransitionService.Setup(x => x.TransitionToInternalStatus(It.IsAny<EApplicationInternalStatus>(), null, It.IsAny<Guid[]>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        //// Act
        var result = await _handler.Handle(new SetSurveyInstructionDetailsRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);        
    }    
}
