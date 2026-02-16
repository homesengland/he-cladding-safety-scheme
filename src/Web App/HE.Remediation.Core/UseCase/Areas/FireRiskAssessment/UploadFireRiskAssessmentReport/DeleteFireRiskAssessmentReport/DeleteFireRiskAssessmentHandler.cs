using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using Mediator;
using System.Transactions;
using HE.Remediation.Core.Data.StoredProcedureParameters;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment.UploadFireRiskAssessmentReport.DeleteFireRiskAssessmentReport
{
    public class DeleteFireRiskAssessmentHandler : IRequestHandler<DeleteFireRiskAssessmentRequest>
    {
        private readonly IFileService _fileService;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IFileRepository _fileRepository;
        private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

        public DeleteFireRiskAssessmentHandler(
            IFileService fileService, 
            IApplicationDataProvider applicationDataProvider, 
            IFileRepository fileRepository, 
            IFireRiskAssessmentRepository fireRiskAssessmentRepository)
        {
            _fileService = fileService;
            _applicationDataProvider = applicationDataProvider;
            _fileRepository = fileRepository;
            _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
        }

        public async ValueTask<Unit> Handle(DeleteFireRiskAssessmentRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _fireRiskAssessmentRepository.DeleteFraForApplication(new DeleteFraForApplicationParameters
                {
                    ApplicationId = applicationId,
                    FileId = request.FileId
                });

                var result = await _fileRepository.DeleteFile(request.FileId);

                await _fileService.DeleteFile($"{request.FileId}{result.Extension}");

                scope.Complete();
            }

            return Unit.Value;
        }
    }
}
