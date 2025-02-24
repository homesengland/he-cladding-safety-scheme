namespace HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;

public class UpsertCostsSchedulingSubcontractorParameters
{
    public Guid? SubcontractorId { get; set; }
    public Guid ApplicationId { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistration { get; set; }
}