namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders;

public class SetProgressReportLeaseholderDateParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public DateTime LastCommunicationDate { get; set; }
}

