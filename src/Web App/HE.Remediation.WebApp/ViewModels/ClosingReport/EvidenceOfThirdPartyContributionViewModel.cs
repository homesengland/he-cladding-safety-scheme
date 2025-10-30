using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class EvidenceOfThirdPartyContributionViewModel : ClosingReportInformationViewModel
    {
        public GetEvidenceDetailsResponse GetEvidenceDetailsResponse { get; set; }
        public Guid ApplicationId { get; set; }
        public bool IsSubmitted { get; set; }
        public bool IsEditable { get; set; }
        public string ReturnUrl { get; set; }
        public string DeleteEndpoint => "DeleteEvidence";
        public string ChangeEndpoint => "ChangeEvidence";

    }
}
 