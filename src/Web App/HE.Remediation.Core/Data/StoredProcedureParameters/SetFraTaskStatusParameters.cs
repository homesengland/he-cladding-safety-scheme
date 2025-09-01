namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetFraTaskStatusParameters
{
    public Guid ApplicationId { get; set; }
    public int TaskStatusId { get; set; }
}