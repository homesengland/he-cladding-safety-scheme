namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetBuildingControlDecisionResult
{
    public bool? BuildingControlRequired { get; set; }
    public DateTime? BuildingControlDecisionDate { get; set; }
    public string BuildingControlDecisionInformation { get; set; }
    public bool? BuildingControlDecision { get; set; }
}