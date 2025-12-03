using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates;

public class SetKeyDatesStatusParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public ETaskStatus TaskStatusId { get; set; }
}