namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class UpdateIntentToProceedTypeParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public int IntentToProceedTypeId { get; set; }
}