using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.DeleteFireRiskAppraisalReport;
using Moq;
using HE.Remediation.Core.Services.StatusTransition;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class DeleteFireRiskAppraisalTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IFileService> _fileService;
    private readonly Mock<IFileRepository> _fileRepository;
    private readonly Mock<IFireRiskAppraisalRepository> _fireRiskAppraisalRepository;
    private readonly Mock<IStatusTransitionService> _statusTransitionService;

    private readonly DeleteFireRiskAppraisalHandler _handler;

    public DeleteFireRiskAppraisalTests()
    {       
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _fileService = new Mock<IFileService>(MockBehavior.Strict);
        _fileRepository = new Mock<IFileRepository>(MockBehavior.Strict);
        _fireRiskAppraisalRepository = new Mock<IFireRiskAppraisalRepository>(MockBehavior.Strict);
        _statusTransitionService = new Mock<IStatusTransitionService>();

        _handler = new DeleteFireRiskAppraisalHandler(_connection.Object,
                                                      _fileService.Object,
                                                      _applicationDataProvider.Object, 
                                                      _fileRepository.Object,
                                                      _fireRiskAppraisalRepository.Object,
                                                      _statusTransitionService.Object);
    }

    [Fact]
    public async Task Handler_Delete_Fire_Risk_Appraisal()
    {
        //Arrange        
        DeleteFileResult fileResult = new DeleteFileResult();
        _connection.Setup(x => x.ExecuteAsync("DeleteFraewForApplication", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _fileRepository.Setup(x => x.DeleteFile(It.IsAny<Guid>()))
                                .ReturnsAsync(fileResult)
                                .Verifiable();        

        _fileService.Setup(x => x.DeleteFile(It.IsAny<string>()))
                                .Returns(Task.CompletedTask)
                                .Verifiable();        

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _fireRiskAppraisalRepository.Setup(x => x.UpdateStatusToInProgress())
                                    .Returns(Task.CompletedTask);

        _statusTransitionService.Setup(x => x.TransitionToInternalStatus(It.IsAny<EApplicationInternalStatus>(), null, It.IsAny<Guid[]>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        //// Act
        var result = await _handler.Handle(new DeleteFireRiskAppraisalRequest(), CancellationToken.None);

        // Assert
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        _fileRepository.Verify(x => x.DeleteFile(It.IsAny<Guid>()), Times.Once);
        _fileService.Verify(x => x.DeleteFile(It.IsAny<string>()), Times.Once);
    }
}
