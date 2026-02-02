using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ProjectDates.Set;

public class SetProjectDatesRequest : IRequest
{
    public int? ProjectStartDateMonth { get; set; }

    public int? ProjectStartDateYear { get; set; }

    public int? ProjectEndDateMonth { get; set; }

    public int? ProjectEndDateYear { get; set; }
}
