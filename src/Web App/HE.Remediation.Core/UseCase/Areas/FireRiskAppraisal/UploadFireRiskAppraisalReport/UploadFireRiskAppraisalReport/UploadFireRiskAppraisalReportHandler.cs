using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.UploadFireRiskAppraisalReport
{
    public class UploadFireRiskAppraisalReportHandler : IRequestHandler<UploadFireRiskAppraisalReportRequest>
    {
        private readonly IFileService _fileService;
        private readonly IFileRepository _fileRepository;
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly FileServiceSettings _fileServiceSettings;

        public UploadFireRiskAppraisalReportHandler(IFileService fileService, IDbConnectionWrapper dbConnection, IApplicationDataProvider applicationDataProvider, IOptionsSnapshot<FileServiceSettings> fileServiceSettings,
            IFileRepository fileRepository)
        {
            _fileService = fileService;
            _dbConnection = dbConnection;
            _applicationDataProvider = applicationDataProvider;
            _fileServiceSettings = fileServiceSettings.Value;
            _fileRepository = fileRepository;
        }

        public async Task<Unit> Handle(UploadFireRiskAppraisalReportRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            if(await CheckFraewAlreadyExists(applicationId))
            {
                return Unit.Value;
            }

            if (request.File == null)
            {
                throw new InvalidFileException("No file selected");
            }

            var fileResult = await _fileService.ProcessFile(request.File, _fileServiceSettings.FireRiskAppraisal);
                       

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _fileRepository.InsertFile(new InsertFileParameters { Extension = Path.GetExtension(request.File.FileName), Id = fileResult.FileId, MimeType = fileResult.MimeType, Name = request.File.FileName, Size = request.File.Length });
                await _dbConnection.ExecuteAsync("InsertFraewForApplication", new { fileResult.FileId, applicationId });
                await _dbConnection.ExecuteAsync("UpdateFireRiskAssessmentAsComplete", new { applicationId });

                scope.Complete();
            }
            
            return Unit.Value;
        }

        public async Task<bool> CheckFraewAlreadyExists(Guid applicationId)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<bool>("CheckFraewExistsForApplication", new { applicationId });
        }
    }
}
