using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetFraewEvidence
{
    public class SetFraewEvidenceHandler : IRequestHandler<SetFraewEvidenceRequest>
    {
        private readonly IApplicationDataProvider _adp;
        private readonly IClosingReportRepository _closingReportRepository;

        public SetFraewEvidenceHandler(
            IApplicationDataProvider adp,
            IClosingReportRepository closingReportRepository)
        {
            _adp = adp;
            _closingReportRepository = closingReportRepository;
        }

        public async Task<Unit> Handle(SetFraewEvidenceRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _adp.GetApplicationId();

            await _closingReportRepository
                    .SetExitFraewDocumentType(applicationId, request.ExitFraewDocumentType);

            await _closingReportRepository
                    .UpsertClosingReportTaskStatus(applicationId, EClosingReportTask.FireRiskAssessment, ETaskStatus.Completed, false);

            return Unit.Value;
        }
    }
}

