namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class GetHasAppliedForBuildingControlParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
}