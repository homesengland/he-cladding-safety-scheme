namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsSchedulingSubcontractor.Subcontractor.Get;

public class GetCostsSchedulingSubcontractorResponse
{
    public Guid? SubcontractorId { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistration { get; set; }
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public bool IsSubmitted { get; set; }
}