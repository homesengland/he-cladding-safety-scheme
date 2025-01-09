namespace HE.Remediation.Core.Helpers;

public class MonthlyCostsCalculationRequest
{
    public decimal? ApprovedGrantFunding { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public IEnumerable<decimal> MonthlyCosts { get; set; }
    public decimal FinalCost { get; set; }
    public decimal CurrentCost { get; set; }
    public decimal AdditionalCost { get; set; }
}
