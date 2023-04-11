using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using HE.Remediation.Core.UseCase.Areas.Application.LeaseholderEvidence.SetLeaseholderEvidence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace HE.Remediation.Core.UseCase.Areas.Application.LeaseHolderEvidence.SetLeaseHolderEvidence
{
    public class SetLeaseholderEvidenceHandler : IRequestHandler<SetLeaseHolderEvidenceRequest, Unit>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IFileService _fileService;
        private readonly IFileRepository _fileRepository;
        private readonly FileServiceSettings _fileServiceSettings;

        public SetLeaseholderEvidenceHandler(
            IDbConnectionWrapper dbConnection, 
            IApplicationDataProvider applicationIdProvider, 
            IFileService fileService, 
            IFileRepository fileRepository, 
            IOptions<FileServiceSettings> fileServiceSettings)
        {
            _applicationDataProvider = applicationIdProvider;
            _dbConnection = dbConnection;
            _fileService = fileService;
            _fileRepository = fileRepository;
            _fileServiceSettings = fileServiceSettings.Value;
        }

        public async Task<Unit> Handle(SetLeaseHolderEvidenceRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            if (request.Completed)
            {
                if (await ValidateCompletion(applicationId))
                {
                    await _dbConnection.ExecuteAsync("UpdateLeaseHolderEngagementToComplete", new { applicationId });

                    return Unit.Value;
                }
                else
                {
                    throw new InvalidFileException("Please add a file");
                }
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var processFileResult = await ProcessFile(request.File);

                var leaseHolderEngagementId = await _dbConnection.QuerySingleOrDefaultAsync<Guid?>("GetLeaseHolderEngagementIdForApplication", new { applicationId });

                await _fileRepository.InsertFile(new InsertFileParameters
                {
                    Id = processFileResult.FileId, 
                    Extension = Path.GetExtension(request.File.FileName),
                    MimeType = processFileResult.MimeType, 
                    Name = request.File.FileName, 
                    Size = request.File.Length
                });
                await _dbConnection.ExecuteAsync("InsertLeaseHolderEngagementFile", new { FileId = processFileResult.FileId, LeaseHolderId = leaseHolderEngagementId });
                scope.Complete();
            }
            return Unit.Value;
        }

        private async Task<bool> ValidateCompletion(Guid applicationId)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<bool>("CheckCanCompleteLeaseHolderEngagement", new { applicationId });
        }

        private async Task<ProcessFileResult> ProcessFile(IFormFile file)
        {
            if(file == null)
            {
                throw new InvalidFileException("No file selected");
            }

           
            return await _fileService.ProcessFile(file, _fileServiceSettings.LeaseHolderEvidence);
        }
                
    }
}
