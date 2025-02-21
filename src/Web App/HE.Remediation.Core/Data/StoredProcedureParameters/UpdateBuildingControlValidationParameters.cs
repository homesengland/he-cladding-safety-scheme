namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class UpdateBuildingControlValidationParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public DateTime? ValidationDate { get; set; }
    public string ValidationInformation { get; set; }
}