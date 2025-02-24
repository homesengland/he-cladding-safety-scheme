namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetBuildingControlValidationResult
{
    public bool? BuildingControlRequired { get; set; }
    public DateTime? BuildingControlValidationDate { get; set; }
    public string BuildingControlValidationInformation { get; set; }
}