using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.DeleteEvidence
{
    public class DeleteEvidenceRequest : IRequest<Unit>
    {
        public Guid ApplicationId { get; set; }
        public Guid EvidenceId { get; set; }
        public bool IsSubmitted { get; set; }
    }
}
