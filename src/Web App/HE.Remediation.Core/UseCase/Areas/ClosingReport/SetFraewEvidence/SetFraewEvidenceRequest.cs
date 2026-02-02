using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetFraewEvidence
{
    public class SetFraewEvidenceRequest(EFireRiskAssessmentType? exitFraewDocumentType) : IRequest
    {
        public EFireRiskAssessmentType? ExitFraewDocumentType { get; } = exitFraewDocumentType;
    }
}