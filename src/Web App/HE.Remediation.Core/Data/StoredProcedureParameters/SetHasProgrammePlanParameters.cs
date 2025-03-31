namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetHasProgrammePlanParameters
{
    public Guid ApplicationId { get; set; }
    public bool HasProgrammePlan { get; set; }
}