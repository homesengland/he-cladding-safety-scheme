using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks.Shared;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class ProjectDatesViewModel : ScheduleOfWorksBaseViewModel
{
    public int? ExpectedProjectStartDateMonth { get; set; }
    
    public int? ExpectedProjectStartDateYear { get; set; }

    public int? ExpectedProjectEndDateMonth { get; set; }

    public int? ExpectedProjectEndDateYear { get; set; }

    public int? ProjectStartDateMonth { get; set; }

    public int? ProjectStartDateYear { get; set; }

    public int? ProjectEndDateMonth { get; set; }

    public int? ProjectEndDateYear { get; set; }
}
