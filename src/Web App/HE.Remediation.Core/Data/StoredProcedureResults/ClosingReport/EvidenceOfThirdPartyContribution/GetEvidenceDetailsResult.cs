using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport.EvidenceOfThirdPartyContribution
{
    public class GetEvidenceDetailsResult
    {
        public Guid ApplicationId { get; set; }
        public Guid Id { get; set; }
        public string ThirdPartyName { get; set; }
        public DateTime? DateOfAttempt { get; set; }
        public EThirdPartyContributionStatusOfAttempt? StatusOfAttempt { get; set; }
        public string TypeOfContribution { get; set; }
        public string AttemptDetails { get; set; }
        public decimal Amount { get; set; }
        public bool IsSubmitted { get; set; }
        public Guid? FileId { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string MimeType { get; set; }
        public int? Size { get; set; }
        public bool IsDeleted { get; set; }
    }
}
