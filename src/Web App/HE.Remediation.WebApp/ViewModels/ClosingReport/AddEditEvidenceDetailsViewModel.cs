using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class AddEditEvidenceDetailsViewModel : ClosingReportInformationViewModel
    {
        public Guid? Id { get; set; }
        public string ThirdPartyName { get; set; }
        public DateTime? DateOfAttempt { get; set; }
        public EThirdPartyContributionStatusOfAttempt? StatusOfAttempt { get; set; }
        public string AttemptDetails { get; set; }
        public EFundingStillPursuing[] TypeOfContribution { get; set; }
        public decimal Amount { get; set; }
        public bool IsEditable { get; set; }

        public Guid? FileId { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }

        public int Step { get; set; }

        public bool ViaCheckAnswer { get; set; }
    }
}
