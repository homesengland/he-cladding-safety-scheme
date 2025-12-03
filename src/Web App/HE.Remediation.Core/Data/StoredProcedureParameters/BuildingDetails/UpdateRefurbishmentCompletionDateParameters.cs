namespace HE.Remediation.Core.Data.StoredProcedureParameters.BuildingDetails;

public class UpdateRefurbishmentCompletionDateParameters
{
    public Guid ApplicationId { get; set; }

    public DateTime? RefurbishmentCompletionDate { get; set; }
}