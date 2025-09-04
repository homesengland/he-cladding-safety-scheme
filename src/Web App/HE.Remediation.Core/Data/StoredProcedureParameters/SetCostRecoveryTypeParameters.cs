namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetCostRecoveryTypeParameters
{
    public Guid ApplicationId { get; set; }
    public int CostRecoveryTypeId { get; set; }
}