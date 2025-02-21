namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class UpdateHasAppliedForBuildingControlParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public bool HasAppliedForBuildingControl { get; set; }
}