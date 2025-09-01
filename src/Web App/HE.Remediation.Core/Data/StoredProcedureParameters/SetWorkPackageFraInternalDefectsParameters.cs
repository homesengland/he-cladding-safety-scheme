namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetWorkPackageFraInternalDefectsParameters
{
    public Guid ApplicationId { get; set; }
    public IEnumerable<int> DefectIds { get; set; }
    public string OtherInternalFireSafetyRisk { get; set; }
}