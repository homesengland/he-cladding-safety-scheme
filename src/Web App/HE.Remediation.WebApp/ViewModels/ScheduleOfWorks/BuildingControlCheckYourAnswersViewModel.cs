using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks.Shared;

using File = HE.Remediation.WebApp.ViewModels.Shared.File;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks
{
    public class BuildingControlCheckYourAnswersViewModel : ScheduleOfWorksBaseViewModel
    {
        public ENoYes? IsBuildingControlApprovalApplied { get; set; }
        public DateTime? BuildingControlApprovalDate { get; set; }
        public ETaskStatus BuildingControlStatusId { get; set; }
        public List<File> AddedFiles { get; set; }
    }
}