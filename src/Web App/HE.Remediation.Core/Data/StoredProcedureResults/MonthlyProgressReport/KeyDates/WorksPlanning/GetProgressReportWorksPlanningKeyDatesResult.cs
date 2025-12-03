namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates.WorksPlanning;

public class GetProgressReportWorksPlanningKeyDatesResult
{
    public DateTime? ExpectedTenderDate { get; set; }
    public DateTime? ExpectedLeadContractorAppointmentDate { get; set; }
    public DateTime? ActualTenderDate { get; set; }
    public DateTime? ActualLeadContractAppointmentDate { get; set; }
    public DateTime? ExpectedWorksPackageSubmissionDate { get; set; }
    public DateTime? PreviousTenderDate { get; set; }
    public DateTime? PreviousContractorAppointmentDate { get; set; }
    public DateTime? PreviousActualTenderDate { get; set; }
    public DateTime? PreviousActualContractorAppointmentDate { get; set; }
    public DateTime? PreviousExpectedWorksPackageSubmissionDate { get; set; }
}