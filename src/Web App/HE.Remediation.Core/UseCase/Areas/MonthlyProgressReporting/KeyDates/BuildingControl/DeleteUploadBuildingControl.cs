using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.BuildingControl;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.BuildingControl;
public class DeleteUploadBuildingControl : IRequestHandler<DeleteUploadBuildingControlRequest>
{

    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingBuildingControlRepository _buildingControlRepository;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;

    public DeleteUploadBuildingControl(
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingBuildingControlRepository buildingControlRepository,
        IFileService fileService,
        IFileRepository fileRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingControlRepository = buildingControlRepository;
        _fileService = fileService;
        _fileRepository = fileRepository;
    }

    public async ValueTask<Unit> Handle(DeleteUploadBuildingControlRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var parameters = new DeleteUploadBuildingControlParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                FileId = request.FileId
            };
            await _buildingControlRepository.DeleteUploadBuildingControlFile(parameters);
            var result = await _fileRepository.DeleteFile(request.FileId);

            await _fileService.DeleteFile($"{request.FileId}{result.Extension}");
            scope.Complete();
        }
        return Unit.Value;
    }
}

public class DeleteUploadBuildingControlRequest : IRequest
{
    public Guid FileId { get; set; }
}
