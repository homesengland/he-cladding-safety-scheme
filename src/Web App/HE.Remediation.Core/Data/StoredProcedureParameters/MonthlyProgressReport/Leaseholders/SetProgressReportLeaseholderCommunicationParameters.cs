namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders;

public class SetProgressReportLeaseholderCommunicationParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public bool HasContacted { get; set; }
}

