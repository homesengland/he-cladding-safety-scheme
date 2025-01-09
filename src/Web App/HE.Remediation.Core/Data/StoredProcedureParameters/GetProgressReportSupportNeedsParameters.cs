namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class GetProgressReportSupportNeedsParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
}