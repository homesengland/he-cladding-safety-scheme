using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling
{
    public class UpdateCostSchedulePreferredContractorLinksParameters
    {
        public EYesNoNonBoolean? PreferredContractorLinks { get; set; }
        public string PreferredContractorLinkAdditionalNotes { get; set; }
    }
}
