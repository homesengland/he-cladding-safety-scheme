namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;

public abstract class SetDatesChangedParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public int? DatesChangedTypeId { get; set; }
    public string DatesChangedReason { get; set; }
}