using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks.Shared;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks
{
    public class TaskListViewModel : ScheduleOfWorksBaseViewModel
    {
        public ETaskStatus LeaseholderEngagementStatusId { get; set; }

        public ETaskStatus BuildingControlStatusId { get; set; }

        public ETaskStatus WorksContractStatusId { get; set; }

        public ETaskStatus ProfileCostsStatusId { get; set; }

        public ETaskStatus DeclarationStatusId { get; set; }
    }
}