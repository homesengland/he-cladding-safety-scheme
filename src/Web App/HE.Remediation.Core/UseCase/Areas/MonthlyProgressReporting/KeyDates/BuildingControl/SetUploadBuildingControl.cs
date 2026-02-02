using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.BuildingControl;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.BuildingControl;

public class SetUploadBuildingControl : IRequestHandler<SetUploadBuildingControlRequest>
{
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingBuildingControlRepository _buildingControlRepository;
    private readonly FileServiceSettings _fileServiceSettings;

    public SetUploadBuildingControl(
        IFileRepository fileRepository,
        IFileService fileService,
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingBuildingControlRepository buildingControlRepository,
        IOptions<FileServiceSettings> fileServiceSettings)
    {
        _fileRepository = fileRepository;
        _fileService = fileService;
        _applicationDataProvider = applicationDataProvider;
        _buildingControlRepository = buildingControlRepository;
        _fileServiceSettings = fileServiceSettings.Value;
    }

    public async ValueTask<Unit> Handle(SetUploadBuildingControlRequest request, CancellationToken cancellationToken)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {

            var fileResult = await _fileService.ProcessFile(request.File, _fileServiceSettings.MonthlyProgressReportingBuildingControlEvidence);

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

            await _buildingControlRepository.InsertProgressReportBuildingControlFile(parameters);

            scope.Complete();
        }

        return Unit.Value;
    }
}

public class SetUploadBuildingControlRequest : IRequest
{
    public IFormFile File { get; set; }
}