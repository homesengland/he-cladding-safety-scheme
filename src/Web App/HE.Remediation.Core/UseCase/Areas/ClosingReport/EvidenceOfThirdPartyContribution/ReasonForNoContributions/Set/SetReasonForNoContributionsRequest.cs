
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.ReasonForNoContributions.Set
{
    public class SetReasonForNoContributionsRequest : IRequest<Unit>
    {
        public string ReasonNoThirdPartyContributions { get; set; }
    }
}
