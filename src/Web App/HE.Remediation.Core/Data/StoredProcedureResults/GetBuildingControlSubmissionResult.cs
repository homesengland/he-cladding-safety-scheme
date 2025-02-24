namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetBuildingControlSubmissionResult
{
    public bool? BuildingControlRequired { get; set; }
    public DateTime? BuildingControlActualSubmissionDate { get; set; }
    public string BuildingControlActualSubmissionInformation { get; set; }
    public string BuildingControlApplicationReference { get; set; }
}