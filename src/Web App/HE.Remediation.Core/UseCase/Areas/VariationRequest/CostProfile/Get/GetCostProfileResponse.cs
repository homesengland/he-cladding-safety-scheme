using HE.Remediation.Core.Data.StoredProcedureResults.ScheduleOfWorks;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.CostProfile.Get;

public class GetCostProfileResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public decimal? ApprovedGrantFunding { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public decimal? UnclaimedGrantFunding { get; set; }

    public int? ProjectDuration { get; set; }

    public decimal? TotalAmount { get; set; }

    public IReadOnlyCollection<GetCostProfileResultItem> CostsProfile { get; set; } = new List<GetCostProfileResultItem>();

    public bool? IsThirdPartyContributionVariation { get; set; }
}
