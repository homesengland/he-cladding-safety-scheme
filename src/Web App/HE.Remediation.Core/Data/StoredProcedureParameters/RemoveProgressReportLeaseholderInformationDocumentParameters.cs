namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class RemoveProgressReportLeaseholderInformationDocumentParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public Guid FileId { get; set; }
}