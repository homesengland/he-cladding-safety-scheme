namespace HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest;

public class OverviewResult
{
    public decimal TotalGrantFunding { get; set; }

    public decimal TotalGrantPaidToDate { get; set; }
    
    public decimal TotalUnclaimedGrant { get; set; }

    public int ProjectDurationInMonths { get; set; }
}
