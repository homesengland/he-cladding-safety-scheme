namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetFraDateParameters
{
    public Guid ApplicationId { get; set; }
    public DateTime FraDate { get; set; }
}