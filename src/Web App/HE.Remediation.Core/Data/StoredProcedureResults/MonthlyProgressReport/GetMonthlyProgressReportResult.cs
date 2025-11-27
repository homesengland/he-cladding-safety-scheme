using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport;

public class GetMonthlyProgressReportResult
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateDue { get; set; }
    public DateTime? DateSubmitted { get; set; }
    public int Version { get; set; }
    public ETaskStatus TaskStatusId { get; set; }

    public ETaskStatus KeyDatesTaskStatusId { get; set; }
    public ETaskStatus ProjectTeamTaskStatusId { get; set; }
    public ETaskStatus ProjectPlanTaskStatusId { get; set; }
    public ETaskStatus LeaseholdersTaskStatusId { get; set; }
    public ETaskStatus SupportTaskStatusId { get; set; }
}