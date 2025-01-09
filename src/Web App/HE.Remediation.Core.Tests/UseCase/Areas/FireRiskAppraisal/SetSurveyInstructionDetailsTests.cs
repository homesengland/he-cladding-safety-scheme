using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.SetSurveyInstructionDetails;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class SetSurveyInstructionDetailsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IApplicationRepository> _applicationRepository;

    private readonly SetSurveyInstructionDetailsHandler _handler;
        
    public SetSurveyInstructionDetailsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _applicationRepository = new Mock<IApplicationRepository>(MockBehavior.Strict);

        _handler = new SetSurveyInstructionDetailsHandler(_connection.Object, _applicationDataProvider.Object, _applicationRepository.Object);
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

        _applicationRepository.Setup(x => x.UpdateInternalStatus(It.IsAny<Guid>(), It.IsAny<EApplicationInternalStatus>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        //// Act
        var result = await _handler.Handle(new SetSurveyInstructionDetailsRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);        
    }    
}
