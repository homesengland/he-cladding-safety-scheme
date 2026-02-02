using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class SetUploadFireRiskAssessmentReportHandler : IRequestHandler<SetUploadFireRiskAssessmentReportRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;
    private readonly FileServiceSettings _settings;

    public SetUploadFireRiskAssessmentReportHandler(
        IApplicationDataProvider applicationDataProvider, 
        IWorkPackageFireRiskAssessmentRepository fireRiskAssessmentRepository, 
        IFileRepository fileRepository, 
        IFileService fileService,
        IOptions<FileServiceSettings> settings)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
        _fileRepository = fileRepository;
        _fileService = fileService;
        _settings = settings.Value;
    }

    public async ValueTask<Unit> Handle(SetUploadFireRiskAssessmentReportRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        if (request.File is not null)
        {
            var fileResult = await _fileService.ProcessFile(request.File, _settings.FireRiskAppraisalReport);

            await _fileRepository.InsertFile(new InsertFileParameters
            {
                Id = fileResult.FileId,
                MimeType = fileResult.MimeType,
                Name = request.File.FileName,
                Extension = Path.GetExtension(request.File.FileName),
                Size = request.File.Length
            });

            await _fireRiskAssessmentRepository.UploadWorkPackageFraReport(new UploadWorkPackageFraReportParameters
            {
                ApplicationId = applicationId,
                FileId = fileResult.FileId
            });
        }

        await _fireRiskAssessmentRepository.SetWorkPackageFireRiskAssessmentType(
            new SetWorkPackageFireRiskAssessmentType
            {
                ApplicationId = applicationId,
                FireRiskAssessmentTypeId = (int)request.FireRiskAssessmentType!.Value
            });

        await _fireRiskAssessmentRepository.SetWorkPacakgeFraTaskStatus(new SetWorkPacakgeFraTaskStatusParameters
        {
            ApplicationId = applicationId,
            TaskStatusId = (int)ETaskStatus.InProgress
        });

        scope.Complete();

        return Unit.Value;
    } 
}

public class SetUploadFireRiskAssessmentReportRequest : IRequest
{
    public IFormFile File { get; set; }
    public EFireRiskAssessmentType? FireRiskAssessmentType { get; set; }
}