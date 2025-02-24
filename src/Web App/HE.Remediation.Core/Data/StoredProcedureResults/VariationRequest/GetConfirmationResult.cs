namespace HE.Remediation.Core.Data.StoredProcedureResults.VariationRequest;

public class GetConfirmationResult
{
    public bool? IsCostVariation { get; set; }

    public bool? IsScopeVariation { get; set; }

    public bool? IsTimescaleVariation { get; set; }

    public bool? IsThirdPartyContributionVariation { get; set; }

    public int? NewEndMonth { get; set; }

    public int? NewEndYear { get; set; }

    public string ChangeOfScope { get; set; }
    public decimal ContributionAmount { get; set; }

    public string ContributionNotes { get; set; }

    public decimal? VariationRequested { get; set; }

    public string VariationSummary { get; set; }
}
