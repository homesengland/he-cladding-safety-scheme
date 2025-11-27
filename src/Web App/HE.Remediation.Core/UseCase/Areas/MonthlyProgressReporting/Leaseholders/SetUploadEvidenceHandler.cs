using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders
{
    public class SetUploadEvidenceHandler : IRequestHandler<SetUploadEvidenceRequest>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFileService _fileService;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IProgressReportingLeaseholdersRepository _progressReportingLeaseholdersRepository;
        private readonly FileServiceSettings _fileServiceSettings;

        public SetUploadEvidenceHandler(
            IFileRepository fileRepository,
            IFileService fileService,
            IApplicationDataProvider applicationDataProvider,
            IProgressReportingLeaseholdersRepository progressReportingLeaseholdersRepository,
            IOptions<FileServiceSettings> fileServiceSettings)
        {
            _fileRepository = fileRepository;
            _fileService = fileService;
            _applicationDataProvider = applicationDataProvider;
            _progressReportingLeaseholdersRepository = progressReportingLeaseholdersRepository;
            _fileServiceSettings = fileServiceSettings.Value;
        }

        public async Task<Unit> Handle(SetUploadEvidenceRequest request, CancellationToken cancellationToken)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var fileResult = await _fileService.ProcessFile(request.File, _fileServiceSettings.MonthlyProgressReportingLeaseholderEvidence);

                await _fileRepository.InsertFile(new InsertFileParameters
                {
                    Id = fileResult.FileId,
                    Extension = Path.GetExtension(request.File.FileName),
                    MimeType = fileResult.MimeType,
                    Name = request.File.FileName,
                    Size = request.File.Length
                });

                var parameters = new SetUploadEvidenceParameters
                {
                    ApplicationId = _applicationDataProvider.GetApplicationId(),
                    ProgressReportId = _applicationDataProvider.GetProgressReportId(),
                    FileId = fileResult.FileId
                };
                await _progressReportingLeaseholdersRepository.SetLeaseholderCommunicationEvidenceFile(parameters);

                scope.Complete();
            }
            return Unit.Value;
        }
    }

    public class SetUploadEvidenceRequest : IRequest
    {
        public IFormFile File { get; set; }
    }
}
