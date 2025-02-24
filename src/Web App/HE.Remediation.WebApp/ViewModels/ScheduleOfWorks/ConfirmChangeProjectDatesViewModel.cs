using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks.Shared;
using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class ConfirmChangeProjectDatesViewModel : ScheduleOfWorksBaseViewModel
{
    public int? ProjectStartDateMonth { get; set; }

    public int? ProjectStartDateYear { get; set; }

    public int? ProjectEndDateMonth { get; set; }

    public int? ProjectEndDateYear { get; set; }

    [Required] public bool? Proceed { get; set; }
}
