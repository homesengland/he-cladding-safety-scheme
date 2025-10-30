using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks.Shared;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class BuildingControlApprovalViewModel : ScheduleOfWorksBaseViewModel
{
    public ENoYes? IsBuildingControlApprovalApplied { get; set; }
}