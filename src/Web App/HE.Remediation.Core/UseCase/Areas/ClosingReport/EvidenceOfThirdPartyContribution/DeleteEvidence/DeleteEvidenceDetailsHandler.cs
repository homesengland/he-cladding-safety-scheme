using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.DeleteEvidence
{
    public class DeleteEvidenceDetailsHandler : IRequestHandler<DeleteEvidenceRequest>
    {
        private readonly IEvidenceOfThirdPartyContributionRepository _thirdPartyEvidenceRepository;
        private readonly IFileService _fileService;
        private readonly IFileRepository _fileRepository;

        public DeleteEvidenceDetailsHandler(
            IEvidenceOfThirdPartyContributionRepository evidenceOfThirdPartyContributionRepository, 
            IFileService fileService,
            IFileRepository fileRepository
        )
        {
            _thirdPartyEvidenceRepository = evidenceOfThirdPartyContributionRepository;
            _fileService = fileService;
            _fileRepository = fileRepository;
        }

        public async ValueTask<Unit> Handle(DeleteEvidenceRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var evidenceDetails = await _thirdPartyEvidenceRepository.GetEvidenceDetails(request.ApplicationId);
            var evidenceDetail = (evidenceDetails?.FirstOrDefault(ed => ed.Id == request.EvidenceId));

            if(evidenceDetail != null)
            {
                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                if (evidenceDetail.FileId != null)
                {
                    // delete file reference
                    await _thirdPartyEvidenceRepository.DeleteThirdPartyEvidenceFile(request.ApplicationId, evidenceDetail.FileId.Value, evidenceDetail.Id);

                    // delete physical file
                    var result = await _fileRepository.DeleteFile(evidenceDetail.FileId.Value);
                    await _fileService.DeleteFile($"{evidenceDetail.FileId.Value}{result.Extension}");
                }
                await _thirdPartyEvidenceRepository.DeleteEvidenceDetails(request);
                scope.Complete();
            }
            return Unit.Value;
        }
    }

}