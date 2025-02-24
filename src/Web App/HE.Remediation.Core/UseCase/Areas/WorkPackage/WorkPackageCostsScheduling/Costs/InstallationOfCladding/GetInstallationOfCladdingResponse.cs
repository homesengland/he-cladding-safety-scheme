namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.InstallationOfCladding;

public class GetInstallationOfCladdingResponse
{
    public decimal? NewCladdingAmount { get; set; }
    public string NewCladdingDescription { get; set; }
    public decimal? ExternalWorksAmount { get; set; }
    public string ExternalWorksDescription { get; set; }
    public decimal? InternalWorksAmount { get; set; }
    public string InternalWorksDescription { get; set; }
    
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
}