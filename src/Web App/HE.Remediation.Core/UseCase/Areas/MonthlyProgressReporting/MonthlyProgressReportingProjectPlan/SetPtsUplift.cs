using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;

public class SetPtsUpliftHandler : IRequestHandler<SetPtsUpliftRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;
    private readonly IProgressReportingProjectPlanRepository _projectPlanRepository;
    private readonly FileServiceSettings _settings;

    public SetPtsUpliftHandler(
        IApplicationDataProvider applicationDataProvider, 
        IFileRepository fileRepository, 
        IFileService fileService, 
        IProgressReportingProjectPlanRepository projectPlanRepository,
        IOptions<FileServiceSettings> settings)
    {
        _applicationDataProvider = applicationDataProvider;
        _fileRepository = fileRepository;
        _fileService = fileService;
        _projectPlanRepository = projectPlanRepository;
        _settings = settings.Value;
    }

    public async ValueTask<Unit> Handle(SetPtsUpliftRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (request.File is null)
        {
            return Unit.Value;
        }

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var fileResult = await _fileService.ProcessFile(request.File, _settings.PtsUplift);

        await _fileRepository.InsertFile(new InsertFileParameters
        {
            Id = fileResult.FileId,
            Extension = Path.GetExtension(request.File.FileName),
            MimeType = fileResult.MimeType,
            Name = request.File.FileName,
            Size = request.File.Length
        });

        await _projectPlanRepository.InsertPtsUpliftDocument(new InsertPtsUpliftDocumentParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            FileId = fileResult.FileId
        });

        scope.Complete();

        return Unit.Value;
    }
}

public class SetPtsUpliftRequest : IRequest
{
    public IFormFile File { get; set; }
}