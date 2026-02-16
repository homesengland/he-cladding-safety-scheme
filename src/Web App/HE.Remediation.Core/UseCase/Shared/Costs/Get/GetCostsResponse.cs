using HE.Remediation.Core.Data.StoredProcedureResults.Costs;

namespace HE.Remediation.Core.UseCase.Shared.Costs.Get;

public class GetCostsResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsPaymentRequest { get; set; } 

    public decimal? TotalGrantFunding { get; set; }

    public DateTime? ProjectStartDate { get; set; }

    public DateTime? ProjectEndDate { get; set; }

    public decimal? ApprovedGrantFunding { get; set; }

    public decimal? ThirdPartyContribution { get; set; }

    public bool IsPtfsPaymentPaid { get; set; }

    public decimal? PtfsPayment { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public decimal MonthlyCostsTotal { get; set; }

    public decimal? TotalGrantPaidToDate { get; set; }

    public decimal? UnprofiledGrantFunding { get; set; }

    public bool IsAdditionalPtfsPaid { get; set; }
    
    public decimal? AdditionalPtfsPayment { get; set; }

    public bool? IsThirdPtfsPaid { get; set; }

    public decimal? ThirdPtfsPayment { get; set; }

    public bool IsPtfsReclaimPaid { get; set; }

    public decimal? PtfsReclaimAmount { get; set; }

    public IReadOnlyCollection<MonthlyCostResult> MonthlyCosts { get; set; } = new List<MonthlyCostResult>();

    public bool IsSubmitted { get; set; }
}
