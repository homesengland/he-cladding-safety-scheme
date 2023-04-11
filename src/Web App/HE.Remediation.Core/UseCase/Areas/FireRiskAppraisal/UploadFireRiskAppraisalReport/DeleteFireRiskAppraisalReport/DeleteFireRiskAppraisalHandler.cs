using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.DeleteFireRiskAppraisalReport
{
    public class DeleteFireRiskAppraisalHandler : IRequestHandler<DeleteFireRiskAppraisalRequest>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IFileService _fileService;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IFileRepository _fileRepository;

        public DeleteFireRiskAppraisalHandler(IDbConnectionWrapper dbConnection, IFileService fileService, IApplicationDataProvider applicationDataProvider, IFileRepository fileRepository)
        {
            _dbConnection = dbConnection;
            _fileService = fileService;
            _applicationDataProvider = applicationDataProvider;
            _fileRepository = fileRepository;
        }

        public async Task<Unit> Handle(DeleteFireRiskAppraisalRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _dbConnection.ExecuteAsync("DeleteFraewForApplication", new { applicationId });
                var result = await _fileRepository.DeleteFile(request.FileId);

                await _fileService.DeleteFile($"{request.FileId}{result.Extension}");

                scope.Complete();
            }

            return Unit.Value;
        }
    }
}
