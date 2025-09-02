namespace HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport.EvidenceOfThirdPartyContribution
{
    public class GetEvidenceForImportResult
    {
        public Guid ImportMarkerId { get; set; }
        public Guid ApplicationId { get; set; }
        public string ThirdPartyName { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public decimal ContributionAmount { get; set; } 
        public string ContributionNotes { get; set; }
        public string TypeOfContributionCsv { get; set; }
    }
}
