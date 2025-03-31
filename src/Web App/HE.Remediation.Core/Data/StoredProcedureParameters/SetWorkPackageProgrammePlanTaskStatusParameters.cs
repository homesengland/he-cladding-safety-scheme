namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetWorkPackageProgrammePlanTaskStatusParameters
{
    public Guid ApplicationId { get; set; }
    public int TaskStatusId { get; set; }
}