namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ConfirmChangeProjectDates.Get;

public class GetConfirmChangeProjectDatesResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }
}
