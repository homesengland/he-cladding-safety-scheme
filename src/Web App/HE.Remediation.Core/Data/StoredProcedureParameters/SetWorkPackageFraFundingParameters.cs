namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetWorkPackageFraFundingParameters
{
    public Guid ApplicationId { get; set; }
    public bool HasFunding { get; set; }
    public int? FundingTypeId { get; set; }
}