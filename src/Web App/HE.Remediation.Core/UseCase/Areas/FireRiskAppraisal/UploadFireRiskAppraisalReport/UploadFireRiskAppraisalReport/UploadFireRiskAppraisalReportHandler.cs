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

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.UploadFireRiskAppraisalReport
{
    public class UploadFireRiskAppraisalReportHandler : IRequestHandler<UploadFireRiskAppraisalReportRequest>
    {
        private readonly IFileService _fileService;
        private readonly IFileRepository _fileRepository;
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly FileServiceSettings _fileServiceSettings;
        private readonly IStatusTransitionService _statusTransitionService;

        private const string FraewFilePropertyName = "Fraew";
        private const string FraewSummaryFilePropertyName = "FraewSummary";

        public UploadFireRiskAppraisalReportHandler(
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

        public async Task<Unit> Handle(UploadFireRiskAppraisalReportRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await ValidateFraew(applicationId, request);
            
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                ProcessFileResult fraewResult = default;
                ProcessFileResult fraewSummaryResult = default;

                if (request.FraewFile is not null)
                {
                    fraewResult = await ProcessFile(request.FraewFile, FraewFilePropertyName, _fileServiceSettings.FireRiskAppraisal);
                }

                if (request.SummaryFile is not null)
                {
                    fraewSummaryResult = await ProcessFile(request.SummaryFile, FraewSummaryFilePropertyName, _fileServiceSettings.FireRiskAppraisalSummary);
                }

                if (request.FraewFile is not null)
                {
                    await _fileRepository.InsertFile(new InsertFileParameters { Extension = Path.GetExtension(request.FraewFile.FileName), Id = fraewResult.FileId, MimeType = fraewResult.MimeType, Name = request.FraewFile.FileName, Size = request.FraewFile.Length });
                    await _dbConnection.ExecuteAsync("InsertFraewForApplication", new { fraewResult.FileId, applicationId });
                }
                if (request.SummaryFile is not null)
                {
                    await _fileRepository.InsertFile(new InsertFileParameters { Extension = Path.GetExtension(request.SummaryFile.FileName), Id = fraewSummaryResult.FileId, MimeType = fraewSummaryResult.MimeType, Name = request.SummaryFile.FileName, Size = request.SummaryFile.Length });
                    await _dbConnection.ExecuteAsync("InsertFraewSummaryForApplication", new { fraewSummaryResult.FileId, applicationId });
                }
                
                await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.FraewUploaded, applicationIds: applicationId);

                scope.Complete();
            }

            return Unit.Value;
        }

        private async Task<ProcessFileResult> ProcessFile(IFormFile file, string propertyName, UploadSectionSettings settings)
        {
            try
            {
                return await _fileService.ProcessFile(file, settings);
            }
            catch(InvalidFileException ex)
            {
                throw new InvalidFileException(ex.Message, propertyName);
            }
        }

        private async Task ValidateFraew(Guid applicationId, UploadFireRiskAppraisalReportRequest request)
        {
            var existingFraew = await _dbConnection.QuerySingleOrDefaultAsync<ExistingFraewResult>("GetExistingFraewForApplication", new { applicationId });

            var errors = new List<KeyValuePair<string, string>>();

            if(existingFraew.FraewFileId == null && request.FraewFile == null && !request.FraewAlreadyUploaded)
            {
                errors.Add(new KeyValuePair<string, string>(FraewFilePropertyName, "FRAEW File Required"));
            }

            if (existingFraew.FraewSummaryFileId == null && request.SummaryFile == null && !request.SummaryAlreadyUploaded)
            {
                errors.Add(new KeyValuePair<string, string>(FraewSummaryFilePropertyName, "FRAEW Summary File Required"));
            }

            if (errors.Any())
            {
                throw new InvalidFileException(errors);
            }
        }
    }
}
