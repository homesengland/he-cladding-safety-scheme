namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectPlan;

public class SetProjectPlanParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public decimal? RemainingAmount { get; set; }
    public bool? EnoughFunds { get; set; }
    public int? IntentToProceedType { get; set; }
    public bool? InternalAdditionalWork { get; set; }
}