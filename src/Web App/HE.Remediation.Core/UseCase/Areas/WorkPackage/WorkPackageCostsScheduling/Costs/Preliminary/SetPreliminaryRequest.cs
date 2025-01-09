using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Preliminary;

public class SetPreliminaryRequest : IRequest
{
    public decimal? MainContractorPreliminariesAmount { get; set; }
    public string MainContractorPreliminariesDescription { get; set; }
    public decimal? AccessAmount { get; set; }
    public string AccessDescription { get; set; }
    public decimal? MainContractorOverheadAmount { get; set; }
    public string MainContractorOverheadDescription { get; set; }
    public decimal? ContractorContingenciesAmount { get; set; }
    public string ContractorContingenciesDescription { get; set; }
}