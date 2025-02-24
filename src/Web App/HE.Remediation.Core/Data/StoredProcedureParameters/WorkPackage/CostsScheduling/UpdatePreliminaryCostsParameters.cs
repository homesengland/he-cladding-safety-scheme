namespace HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;

public class UpdatePreliminaryCostsParameters
{
    public decimal MainContractorPreliminariesAmount { get; set; }
    public string MainContractorPreliminariesDescription { get; set; }
    public decimal AccessAmount { get; set; }
    public string AccessDescription { get; set; }
    public decimal OverheadsAndProfitAmount { get; set; }
    public string OverheadsAndProfitDescription { get; set; }
    public decimal ContractorContingenciesAmount { get; set; }
    public string ContractorContingenciesDescription{ get; set; }
}