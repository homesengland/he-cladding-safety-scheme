using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults
{
    public class FireRiskWorksRequiredResult
    {
        public ENoYes? ExternalWorksRequired { get; set; }
        public ENoYes? InternalWorksRequired { get; set; }
    }
}
