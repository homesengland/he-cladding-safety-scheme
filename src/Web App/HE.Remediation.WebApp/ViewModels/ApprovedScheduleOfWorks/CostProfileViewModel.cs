using HE.Remediation.WebApp.ViewModels.ApprovedScheduleOfWorks.Shared;

namespace HE.Remediation.WebApp.ViewModels.ApprovedScheduleOfWorks;

public class CostProfileViewModel : ApprovedScheduleOfWorksBaseViewModel
{
    public decimal? TotalGrantFunding { get; set; }

    public decimal? TotalGrantPaidToDate { get; set; }

    public decimal? TotalUnclaimedGrant { get; set; }

    public int? ProjectDuration { get; set; }

    public decimal? TotalSubmittedValue { get; set; }

    public decimal? TotalConfirmedValue { get; set; }
    
    public IReadOnlyCollection<CostProfileItemViewModel> CostsProfile { get; set; }
}
