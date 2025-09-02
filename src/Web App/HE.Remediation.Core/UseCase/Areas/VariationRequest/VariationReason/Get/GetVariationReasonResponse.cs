namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.VariationReason.Get;

public class GetVariationReasonResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool? IsCostVariation { get; set; }

    public bool? IsScopeVariation { get; set; }

    public bool? IsTimescaleVariation { get; set; }

    public bool? IsThirdPartyContributionVariation { get; set; }

    public decimal? ApprovedGrantFunding { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public decimal? UnclaimedGrantFunding { get; set; }

    public bool IsSubmitted { get; set; }

    public bool ClosingReportStarted { get; set; }

    public bool LastMonthlyPaymentCompleted { get; set; }

}
