namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetLatestCostScheduleResult
{
    public decimal? FraewSurveyAmount { get; set; }
    public decimal? RemovalOfCladdingAmount { get; set; }
    public string RemovalOfCladdingDescription { get; set; }
    public decimal? NewCladdingAmount { get; set; }
    public string NewCladdingDescription { get; set; }
    public decimal? OtherEligibleWorkToExternalWallAmount { get; set; }
    public string OtherEligibleWorkToExternalWallDescription { get; set; }
    public decimal? InternalMitigationWorksAmount { get; set; }
    public string InternalMitigationWorksDescription { get; set; }
    public decimal? MainContractorPreliminariesAmount { get; set; }
    public string MainContractorPreliminariesDescription { get; set; }
    public decimal? AccessAmount { get; set; }
    public string AccessDescription { get; set; }
    public decimal? OverheadsAndProfitAmount { get; set; }
    public string OverheadsAndProfitDescription { get; set; }
    public decimal? ContractorContingenciesAmount { get; set; }
    public string ContractorContingenciesDescription { get; set; }
    public decimal? FeasibilityStageAmount { get; set; }
    public string FeasibilityStageDescription { get; set; }
    public decimal? PostTenderStageAmount { get; set; }
    public string PostTenderStageDescription { get; set; }
    public decimal? IrrecoverableVatAmount { get; set; }
    public string IrrecoverableVatDescription { get; set; }
    public decimal? PropertyManagerAmount { get; set; }
    public string PropertyManagerDescription { get; set; }
    public decimal? IneligibleAmount { get; set; }
    public string IneligibleDescription { get; set; }
}