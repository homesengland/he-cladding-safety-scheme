namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetWorkPackageFraDateParameters
{
    public Guid ApplicationId { get; set; }
    public DateTime FraDate { get; set; }
}