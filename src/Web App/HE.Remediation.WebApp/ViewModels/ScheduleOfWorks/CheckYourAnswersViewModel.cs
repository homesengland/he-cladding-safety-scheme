using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks.Shared;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class CheckYourAnswersViewModel : ScheduleOfWorksBaseViewModel
{
    public DateTime? ProjectStartDate { get; set; }

    public DateTime? ProjectEndDate { get; set; }

    public decimal? ApprovedGrantFunding { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public decimal? ProfiledPayments { get; set; }

    public int? Duration { get; set; }
    
    public IReadOnlyCollection<string> ContractFileNames { get; set; }
    public IReadOnlyCollection<string> BuildingControlFileNames { get; set; }
    public IReadOnlyCollection<string> LeaseholderEngagementFileNames { get; set; }
}
