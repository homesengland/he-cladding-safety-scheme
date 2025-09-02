namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetFraFundingParameters
{
    public Guid ApplicationId { get; set; }
    public bool HasFunding { get; set; }
    public int FundingTypeId { get; set; }
}