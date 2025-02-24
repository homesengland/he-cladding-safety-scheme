namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class UpdateBuildingControlForecastParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public DateTime? ForecastDate { get; set; }
    public string ForecastInformation { get; set; }
}