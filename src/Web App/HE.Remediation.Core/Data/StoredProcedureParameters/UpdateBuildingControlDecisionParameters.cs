namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class UpdateBuildingControlDecisionParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public DateTime? DecisionDate { get; set; }
    public string DecisionInformation { get; set; }
    public bool? BuildingControlDecision { get; set; }
}