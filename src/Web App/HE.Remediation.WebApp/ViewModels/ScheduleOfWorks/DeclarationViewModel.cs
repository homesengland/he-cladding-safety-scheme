using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks.Shared;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class DeclarationViewModel : ScheduleOfWorksBaseViewModel
{
    public bool? ConfirmedAccuratelyProfiledCosts { get; set; }
    public bool? ConfirmedAwareOfProcess { get; set; }
    public bool? ConfirmedAwareOfVariationApproval { get; set; }
    public DateTime? ProjectStartDate { get; set; }
}
