namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class UpdateBankAccountVerificationContactParameters
{
    public Guid ApplicationId { get; set; }
    public string ContactName { get; set; }
    public string ContactNumber { get; set; }
}