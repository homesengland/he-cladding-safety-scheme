namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ProjectDates.Get;

public class GetProjectDatesResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public DateTime? ExpectedProjectStartDate { get; set; }

    public DateTime? ExpectedProjectEndDate { get; set; }

    public int? ProjectStartDateMonth { get; set; }

    public int? ProjectStartDateYear { get; set; }

    public int? ProjectEndDateMonth { get; set; }

    public int? ProjectEndDateYear { get; set; }

    public bool IsSubmitted { get; set; }
}
