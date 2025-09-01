namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetWorkPackageFraAssessorAndDateParameters
{
    public Guid ApplicationId { get; set; }
    public int AssessorId { get; set; }
    public DateTime FraDate { get; set; }
}