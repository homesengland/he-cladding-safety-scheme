using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;
using System.Transactions;
using HE.Remediation.Core.Services.StatusTransition;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAssessmentReport.DeleteFireRiskAssessmentReport
{
    public class DeleteFireRiskAssessmentHandler : IRequestHandler<DeleteFireRiskAssessmentRequest>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IFileService _fileService;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IFileRepository _fileRepository;
        private readonly IFireRiskAppraisalRepository _fireRiskAppraisalRepository;
        private readonly IStatusTransitionService _statusTransitionService;

        public DeleteFireRiskAssessmentHandler(
            IDbConnectionWrapper dbConnection, 
            IFileService fileService, 
            IApplicationDataProvider applicationDataProvider, 
            IFileRepository fileRepository, 
            IFireRiskAppraisalRepository fireRiskAppraisalRepository, 
            IStatusTransitionService statusTransitionService)
        {
            _dbConnection = dbConnection;
            _fileService = fileService;
            _applicationDataProvider = applicationDataProvider;
            _fileRepository = fileRepository;
            _fireRiskAppraisalRepository = fireRiskAppraisalRepository;
            _statusTransitionService = statusTransitionService;
        }

        public async Task<Unit> Handle(DeleteFireRiskAssessmentRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _dbConnection.ExecuteAsync("DeleteFraForApplication", new { ApplicationId = applicationId, request.FileId });
                var result = await _fileRepository.DeleteFile(request.FileId);

                await _fileService.DeleteFile($"{request.FileId}{result.Extension}");

                await _fireRiskAppraisalRepository.UpdateStatusToInProgress();

                await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.FraewInstructed, applicationIds: applicationId);

                scope.Complete();
            }

            return Unit.Value;
        }
    }
}
