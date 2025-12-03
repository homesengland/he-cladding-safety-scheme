namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.WorksPlanning;

public class SetProgressReportWorksPlanningKeyDatesParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public DateTime? ExpectedTenderDate { get; set; }
    public DateTime? ExpectedLeadContractorDate { get; set; }
    public DateTime? ActualTenderDate { get; set; }
    public DateTime? ActualLeadContractorDate { get; set; }
    public DateTime? ExpectedWorksPackageSubmissionDate { get; set; }
}