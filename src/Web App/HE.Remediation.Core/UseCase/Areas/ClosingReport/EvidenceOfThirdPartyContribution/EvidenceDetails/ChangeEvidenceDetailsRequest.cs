using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails
{
    public class ChangeEvidenceDetailsRequest
    {
        public Guid EvidenceId { get; set; }
        public Guid ApplicationId { get; set; }
        public string ThirdPartyName { get; set; }
        public string AttemptDetails { get; set; }
        public string DateOfAttempt { get; set; }
        public EThirdPartyContributionStatusOfAttempt EThirdPartyContributionStatusOfAttempt { get; set; }
    }
}
