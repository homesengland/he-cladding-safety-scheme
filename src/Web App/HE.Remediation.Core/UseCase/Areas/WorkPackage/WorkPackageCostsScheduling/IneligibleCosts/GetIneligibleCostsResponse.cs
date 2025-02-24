namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.IneligibleCosts;

public class GetIneligibleCostsResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    
    public decimal? IneligibleAmount { get; set; }
    
    public string IneligibleDescription { get; set; }

    public bool IsSubmitted { get; set; }
}