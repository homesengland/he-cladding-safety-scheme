using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Transactions;
using HE.Remediation.Core.Services.StatusTransition;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAssessmentReport.UploadFireRiskAssessmentReport
{
    public class UploadFireRiskAssessmentReportHandler : IRequestHandler<UploadFireRiskAssessmentReportRequest>
    {
        private readonly IFileService _fileService;
        private readonly IFileRepository _fileRepository;
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly FileServiceSettings _fileServiceSettings;
        private readonly IStatusTransitionService _statusTransitionService;

        private const string FraReportFilePropertyName = "FraReport";

        public UploadFireRiskAssessmentReportHandler(
            IFileService fileService, 
            IDbConnectionWrapper dbConnection, 
            IApplicationDataProvider applicationDataProvider, 
            IOptions<FileServiceSettings> fileServiceSettings,
            IFileRepository fileRepository, 
            IStatusTransitionService statusTransitionService)
        {
            _fileService = fileService;
            _dbConnection = dbConnection;
            _applicationDataProvider = applicationDataProvider;
            _fileServiceSettings = fileServiceSettings.Value;
            _fileRepository = fileRepository;
            _statusTransitionService = statusTransitionService;
        }

        public async Task<Unit> Handle(UploadFireRiskAssessmentReportRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (request.FraReportFile is not null)
                {
                    var fraReportResult = await ProcessFile(request.FraReportFile, _fileServiceSettings.FireRiskAppraisalReport);
                    await _fileRepository.InsertFile(new InsertFileParameters
                    {
                        Extension = Path.GetExtension(request.FraReportFile.FileName), 
                        Id = fraReportResult.FileId, 
                        MimeType = fraReportResult.MimeType, 
                        Name = request.FraReportFile.FileName, 
                        Size = request.FraReportFile.Length
                    });

                    await _dbConnection.ExecuteAsync("InsertFraReportForApplication", new { fraReportResult.FileId, applicationId });
                }

                await _dbConnection.ExecuteAsync("UpdateFireRiskAssessmentType",
                    new
                    {
                        ApplicationId = applicationId,
                        FireRiskAssessmentTypeId = (int?)request.FireRiskAssessmentType
                    });

                await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.FraewUploaded, applicationIds: applicationId);

                scope.Complete();
            }

            return Unit.Value;
        }

        private async Task<ProcessFileResult> ProcessFile(IFormFile file, UploadSectionSettings settings)
        {
            try
            {
                return await _fileService.ProcessFile(file, settings);
            }
            catch(InvalidFileException ex)
            {
                throw new InvalidFileException(ex.Message, FraReportFilePropertyName);
            }
        }
    }
}
