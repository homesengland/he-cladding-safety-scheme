using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.SetAppraisalSurveyDetails;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class SetAppraisalSurveyDetailsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IApplicationRepository> _applicationRepository;

    private readonly SetAppraisalSurveyDetailsHandler _handler;
        
    public SetAppraisalSurveyDetailsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _applicationRepository = new Mock<IApplicationRepository>(MockBehavior.Strict);

        _handler = new SetAppraisalSurveyDetailsHandler(_connection.Object,
                                                        _applicationDataProvider.Object,
                                                        _applicationRepository.Object);
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

        _applicationRepository.Setup(x => x.GetApplicationStatus(It.IsAny<Guid>()))
            .Returns(Task.FromResult(new GetApplicationStatusResult
            {
                DeclarationConfirmed = false,
                Stage = EApplicationStage.ApplyForGrant,
                Status = EApplicationStatus.ApplicationInProgress,
                Submitted = false
            }))
            .Verifiable();

        _applicationRepository.Setup(x => x.UpdateInternalStatus(It.IsAny<Guid>(), It.IsAny<EApplicationInternalStatus>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        //// Act
        var result = await _handler.Handle(new SetAppraisalSurveyDetailsRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);        
    }    
}
