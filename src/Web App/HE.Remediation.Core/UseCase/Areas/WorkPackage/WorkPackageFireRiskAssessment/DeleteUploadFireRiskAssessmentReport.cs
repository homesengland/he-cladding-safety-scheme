using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class DeleteUploadFireRiskAssessmentReportHandler : IRequestHandler<DeleteUploadFireRiskAssessmentReportRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;

    public DeleteUploadFireRiskAssessmentReportHandler(
        IApplicationDataProvider applicationDataProvider, 
        IWorkPackageFireRiskAssessmentRepository fireRiskAssessmentRepository, 
        IFileRepository fileRepository, 
        IFileService fileService)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
        _fileRepository = fileRepository;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(DeleteUploadFireRiskAssessmentReportRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _fireRiskAssessmentRepository.DeleteWorkPackageFraReport(new DeleteWorkPackageFraReportParameters
        {
            ApplicationId = applicationId,
            FileId = request.FileId
        });

        var deleteResult = await _fileRepository.DeleteFile(request.FileId);

        await _fileService.DeleteFile($"{request.FileId}{deleteResult.Extension}");

        scope.Complete();

        return Unit.Value;
    }
}

public class DeleteUploadFireRiskAssessmentReportRequest : IRequest
{
    public Guid FileId { get; set; }
}