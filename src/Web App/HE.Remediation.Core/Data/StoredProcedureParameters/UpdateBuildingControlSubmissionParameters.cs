namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class UpdateBuildingControlSubmissionParameters
{
    public Guid ApplicationId { get; set; }
    public Guid ProgressReportId { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public string SubmissionInformation { get; set; }
    public string BuildingControlApplicationReference { get; set; }
}