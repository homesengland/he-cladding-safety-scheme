namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders;

public class GetProgressReportLeaseholderCommunicationParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
}

