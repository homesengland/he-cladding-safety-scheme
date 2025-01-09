namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.UnsafeCladding;

public class GetUnsafeCladdingCostsResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public decimal? UnsafeCladdingRemovalAmount { get; set; }
    public string UnsafeCladdingRemovalDescription { get; set; }

    public bool IsSubmitted { get; set; }
}