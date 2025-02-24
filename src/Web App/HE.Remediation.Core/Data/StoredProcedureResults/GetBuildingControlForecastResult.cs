namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetBuildingControlForecastResult
{
    public bool? BuildingControlRequired { get; set; }
    public DateTime? BuildingControlForecastSubmissionDate { get; set; }
    public string BuildingControlForecastInformation { get; set; }
}