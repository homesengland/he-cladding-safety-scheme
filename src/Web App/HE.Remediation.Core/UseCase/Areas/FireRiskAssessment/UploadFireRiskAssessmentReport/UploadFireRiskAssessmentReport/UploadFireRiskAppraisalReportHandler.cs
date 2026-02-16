using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment.UploadFireRiskAssessmentReport.UploadFireRiskAssessmentReport
{
    public class UploadFireRiskAssessmentReportHandler : IRequestHandler<UploadFireRiskAssessmentReportRequest>
    {
        private readonly IFileService _fileService;
        private readonly IFileRepository _fileRepository;
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly FileServiceSettings _fileServiceSettings;

        private const string FraReportFilePropertyName = "FraReport";

        public UploadFireRiskAssessmentReportHandler(
            IFileService fileService,
            IDbConnectionWrapper dbConnection,
            IApplicationDataProvider applicationDataProvider,
            IOptions<FileServiceSettings> fileServiceSettings,
            IFileRepository fileRepository)
        {
            _fileService = fileService;
            _dbConnection = dbConnection;
            _applicationDataProvider = applicationDataProvider;
            _fileServiceSettings = fileServiceSettings.Value;
            _fileRepository = fileRepository;
        }

        public async ValueTask<Unit> Handle(UploadFireRiskAssessmentReportRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();


            if (request.FraReportFile is not null)
            {
                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                var fraReportResult = await ProcessFile(request.FraReportFile, _fileServiceSettings.FireRiskAppraisalReport);

                await _fileRepository.InsertFile(new InsertFileParameters
                {
                    Extension = Path.GetExtension(request.FraReportFile.FileName),
                    Id = fraReportResult.FileId,
                    MimeType = fraReportResult.MimeType,
                    Name = request.FraReportFile.FileName,
                    Size = request.FraReportFile.Length
                });

                await _dbConnection.ExecuteAsync("InsertFraReportForApplication",
                    new { fraReportResult.FileId, applicationId });

                scope.Complete();
            }
            else
            {
                await _dbConnection.ExecuteAsync("UpdateFireRiskAssessmentType",
                    new
                    {
                        ApplicationId = applicationId,
                        FireRiskAssessmentTypeId = (int?)request.FireRiskAssessmentType
                    });
            }

            
            return Unit.Value;
        }

        private async ValueTask<ProcessFileResult> ProcessFile(IFormFile file, UploadSectionSettings settings)
        {
            try
            {
                return await _fileService.ProcessFile(file, settings);
            }
            catch (InvalidFileException ex)
            {
                throw new InvalidFileException(ex.Message, FraReportFilePropertyName);
            }
        }
    }
}
