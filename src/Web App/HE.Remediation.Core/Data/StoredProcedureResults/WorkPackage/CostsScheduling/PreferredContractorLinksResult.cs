using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.CostsScheduling
{
    public class PreferredContractorLinksResult
    {
        public EYesNoNonBoolean? PreferredContractorLinks { get; set; }
        public string PreferredContractorLinkAdditionalNotes { get; set; }
        public ENoYes? SoughtQuotes { get; set; }
    }
}
