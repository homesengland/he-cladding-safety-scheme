namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.TotalCosts;

public class GetTotalCostsResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    
    public decimal? UnsafeCladdingRemovalAmount { get; set; }
    
    public decimal? NewCladdingAmount { get; set; }
    public decimal? ExternalWorksAmount { get; set; }
    public decimal? InternalWorksAmount { get; set; }
    
    public decimal? MainContractorPreliminariesAmount { get; set; }
    public decimal? AccessAmount { get; set; }
    public decimal? MainContractorOverheadAmount { get; set; }
    public decimal? ContractorContingenciesAmount { get; set; }
    
    public decimal? FraewSurveyAmount { get; set; }
    public decimal? FeasibilityStageAmount { get; set; }
    public decimal? PostTenderStageAmount { get; set; }
    public decimal? PropertyManagerAmount { get; set; }
    public decimal? IrrecoverableVatAmount { get; set; }
    
    public decimal? IneligibleAmount { get; set; }

    public bool IsSubmitted { get; set; }
}