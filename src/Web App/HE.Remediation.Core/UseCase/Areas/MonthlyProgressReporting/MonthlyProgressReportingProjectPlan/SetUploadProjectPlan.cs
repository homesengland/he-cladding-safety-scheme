using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;

public class SetUploadProjectPlan : IRequestHandler<SetUploadProjectPlanRequest>
{
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectPlanRepository _projectPlanRepository;
    private readonly FileServiceSettings _fileServiceSettings;

    public SetUploadProjectPlan(
        IFileRepository fileRepository,
        IFileService fileService,
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectPlanRepository projectPlanRepository,
        IOptions<FileServiceSettings> fileServiceSettings)
    {
        _fileRepository = fileRepository;
        _fileService = fileService;
        _applicationDataProvider = applicationDataProvider;
        _projectPlanRepository = projectPlanRepository;
        _fileServiceSettings = fileServiceSettings.Value;
    }

    public async Task<Unit> Handle(SetUploadProjectPlanRequest request, CancellationToken cancellationToken)
    {
        if (request.File is null)
        {
            return Unit.Value;
        }

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {

            var fileResult = await _fileService.ProcessFile(request.File, _fileServiceSettings.MonthlyProgressReportingProjectPlanEvidence);

            await _fileRepository.InsertFile(new InsertFileParameters
            {
                Id = fileResult.FileId,
                Extension = Path.GetExtension(request.File.FileName),
                MimeType = fileResult.MimeType,
                Name = request.File.FileName,
                Size = request.File.Length
            });

            var parameters = new InsertProgressReportProjectPlanFileParameters
            {
                ApplicationId = _applicationDataProvider.GetApplicationId(),
                ProgressReportId = _applicationDataProvider.GetProgressReportId(),
                FileId = fileResult.FileId
            };

            await _projectPlanRepository.InsertProgressReportProjectPlanFile(parameters);

            scope.Complete();
        }
        return Unit.Value;
    }
}

public class SetUploadProjectPlanRequest : IRequest
{
    public IFormFile File { get; set; }
}