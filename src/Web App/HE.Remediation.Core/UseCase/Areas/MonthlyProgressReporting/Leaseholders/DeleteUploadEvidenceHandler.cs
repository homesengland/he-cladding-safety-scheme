using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders
{
    public class DeleteUploadEvidenceHandler : DeleteUploadEvidenceService, IRequestHandler<DeleteUploadEvidenceRequest>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;

        public DeleteUploadEvidenceHandler(
            IFileRepository fileRepository,
            IFileService fileService,
            IApplicationDataProvider applicationDataProvider,
            IProgressReportingLeaseholdersRepository progressReportingLeaseholdersRepository, 
            ILogger<DeleteUploadEvidenceHandler> logger) 
                : base(fileRepository, fileService, progressReportingLeaseholdersRepository, logger)
        {
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(DeleteUploadEvidenceRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            var progressReportId = _applicationDataProvider.GetProgressReportId();

            await DeleteFile(applicationId, progressReportId, request.FileId);

            return Unit.Value;
        }
    }

    public class DeleteUploadEvidenceRequest : IRequest
    {
        public Guid FileId { get; set; }
    }
}
