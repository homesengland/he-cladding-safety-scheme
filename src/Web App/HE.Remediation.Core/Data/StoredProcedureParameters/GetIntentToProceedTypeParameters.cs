namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class GetIntentToProceedTypeParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
}