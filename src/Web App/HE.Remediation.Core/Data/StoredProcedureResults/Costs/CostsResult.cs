namespace HE.Remediation.Core.Data.StoredProcedureResults.Costs;

public class CostsResult
{
    public DateTime? ProjectStartDate { get; set; }

    public DateTime? ProjectEndDate { get; set; }

    public decimal? ApprovedGrantFunding { get; set; }
    public decimal? ThirdPartyContribution { get; set; }

    public bool? IsPtfsPaymentPaid { get; set; }

    public decimal? PtfsPayment { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public bool? IsAdditionalPtfsPaid { get; set; }

    public decimal? AdditionalPtfsPayment { get; set; }

    public bool? IsThirdPtfsPaid { get; set; }

    public decimal? ThirdPtfsPayment { get; set; }

    public decimal? PtfsReclaimAmount { get; set; }

    public IList<MonthlyCostResult> MonthlyCosts { get; set; } = new List<MonthlyCostResult>();
}
