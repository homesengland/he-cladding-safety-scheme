using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks.Shared;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class BuildingControlApprovalDateViewModel : ScheduleOfWorksBaseViewModel
{
    public int? ApprovalDateDay { get; set; }
    public int? ApprovalDateMonth { get; set; }
    public int? ApprovalDateYear { get; set; }
}