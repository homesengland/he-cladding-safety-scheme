using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails
{
    public class SetEvidenceDetailRequest : IRequest<SetEvidenceDetailResponse>
    {
        public Guid ApplicationId { get; set; }
        public Guid? Id { get; set; }
        public string ThirdPartyName { get; set; }
        public DateTime? DateOfAttempt { get; set; }
        public EThirdPartyContributionStatusOfAttempt? StatusOfAttempt { get; set; }
        public string AttemptDetails { get; set; }
        public EFundingStillPursuing[] TypeOfContribution { get; set; }
        public decimal Amount { get; set; }
    }
}
