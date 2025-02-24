using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.DeleteLeaseHolderEvidence
{
    public class DeleteLeaseHolderEvidenceHandler : IRequestHandler<DeleteLeaseHolderEvidenceRequest>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IFileService _fileService;
        private readonly IFileRepository _fileRepository;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public DeleteLeaseHolderEvidenceHandler(
            IDbConnectionWrapper dbConnection, 
            IFileService fileService, 
            IFileRepository fileRepository, 
            IApplicationDataProvider applicationDataProvider
        )
        {
            _dbConnection = dbConnection;
            _fileService = fileService;
            _fileRepository = fileRepository;
            _applicationDataProvider = applicationDataProvider;
        }
        public async Task<Unit> Handle(DeleteLeaseHolderEvidenceRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _dbConnection.ExecuteAsync("DeleteLeaseHolderEvidence", new { ApplicationId = applicationId, request.FileId });
                var result = await _fileRepository.DeleteFile(request.FileId);

                await _fileService.DeleteFile($"{request.FileId}{result.Extension}");
                scope.Complete();
            }
            return Unit.Value;
        }
    }
}
