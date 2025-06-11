namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class UpdateHasAcquiredRightToManageParameters
{
    public Guid ApplicationId { get; set; }
    public bool HasAcquiredRightToManage { get; set; }
}