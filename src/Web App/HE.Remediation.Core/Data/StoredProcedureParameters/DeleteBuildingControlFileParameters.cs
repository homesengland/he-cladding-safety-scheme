namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class DeleteBuildingControlFileParameters
{
    public Guid ApplicationId { get; set; }
    public Guid FileId { get; set; }
}