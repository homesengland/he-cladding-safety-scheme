using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;
using System.Transactions;
using HE.Remediation.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.AddFile;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails
{
    public class GetEvidenceSubmissionUploadHandler : IRequestHandler<GetEvidenceSubmissionUploadRequest, GetEvidenceSubmissionUploadResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IClosingReportRepository _closingReportRepository;
        private readonly FileServiceSettings _fileServiceSettings;
        private const string EvidenceSubmissionFilePropertyName = "ETPC";
        private readonly IFileService _fileService;
        private readonly IFileRepository _fileRepository;
        private readonly IDbConnectionWrapper _dbConnection;

        public GetEvidenceSubmissionUploadHandler(
            IApplicationDataProvider applicationDataProvider,
            IApplicationRepository applicationRepository,
            IClosingReportRepository closingReportRepository,
            IOptions<FileServiceSettings> fileServiceSettings,
            IFileService fileService,
            IFileRepository fileRepository,
            IDbConnectionWrapper dbConnection)
        {
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _closingReportRepository = closingReportRepository;
            _fileServiceSettings = fileServiceSettings.Value;
            _fileService = fileService;
            _fileRepository = fileRepository;
            _dbConnection = dbConnection;
        }
        public async ValueTask<GetEvidenceSubmissionUploadResponse> Handle(GetEvidenceSubmissionUploadRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);


            var evidenceSubmissionFile = await _closingReportRepository.GetApplicationEvidenceOfThirdPartyContributionFile(applicationId, request.UploadType);

            if (evidenceSubmissionFile != null)
            {
                return new GetEvidenceSubmissionUploadResponse
                {
                    EvidenceSubmissionFile = evidenceSubmissionFile.EvidenceSubmissionFile
                };
            }
            else
            {
                return new GetEvidenceSubmissionUploadResponse();
            }

        }

        public async ValueTask<Unit> Handle(AddFileRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            // Get upload section settings for closing report
            var uploadSettings = _fileServiceSettings.ClosingReport;

            // Process the uploaded file
            ProcessFileResult processResult;
            try
            {
                processResult = await ProcessFile(request.File, EvidenceSubmissionFilePropertyName, uploadSettings);
            }
            catch (InvalidFileException ex)
            {
                throw new InvalidFileException(ex.Message, nameof(request.File));
            }

            // Insert file record in DB
            await _closingReportRepository.InsertFile(applicationId, processResult.FileId, request.UploadType);

            return Unit.Value;
        }



        private async ValueTask<ProcessFileResult> ProcessFile(IFormFile file, string propertyName, UploadSectionSettings settings)
        {
            try
            {
                return await _fileService.ProcessFile(file, settings);
            }
            catch (InvalidFileException ex)
            {
                throw new InvalidFileException(ex.Message, propertyName);
            }
        }
    }
}
