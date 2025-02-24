namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Preliminary;

public class GetPreliminaryResponse
{
    public decimal? MainContractorPreliminariesAmount { get; set; }
    public string MainContractorPreliminariesDescription { get; set; }
    public decimal? AccessAmount { get; set; }
    public string AccessDescription { get; set; }
    public decimal? MainContractorOverheadAmount { get; set; }
    public string MainContractorOverheadDescription { get; set; }
    public decimal? ContractorContingenciesAmount { get; set; }
    public string ContractorContingenciesDescription { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }
}