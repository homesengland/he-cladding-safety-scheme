using HE.Remediation.Core.Data.StoredProcedureResults.ScheduleOfWorks;

namespace HE.Remediation.Core.UseCase.Areas.ApprovedScheduleOfWorks.CostProfile.Get;

public class GetCostProfileResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public decimal TotalGrantFunding { get; set; }

    public decimal TotalGrantPaidToDate { get; set; }

    public decimal TotalUnclaimedGrant { get; set; }

    public int ProjectDuration { get; set; }

    public decimal? TotalSubmittedValue { get; set; }

    public decimal? TotalConfirmedValue { get; set; }

    public IReadOnlyCollection<CostsProfileResult> CostsProfile { get; set; } = new List<CostsProfileResult>();
}
