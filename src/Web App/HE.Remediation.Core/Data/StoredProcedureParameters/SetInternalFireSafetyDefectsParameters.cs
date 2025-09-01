using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetInternalFireSafetyDefectsParameters
{
    public Guid ApplicationId { get; set; }
    public IEnumerable<EInternalFireSafetyDefect> InternalFireSafetyDefectIds { get; set; }
    public string OtherInternalFireSafetyRisk { get; set; }
}