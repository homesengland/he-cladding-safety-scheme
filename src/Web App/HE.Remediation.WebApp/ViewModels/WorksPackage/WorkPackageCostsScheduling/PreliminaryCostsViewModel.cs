using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class PreliminaryCostsViewModel : WorkPackageBaseViewModel
{
    public string MainContractorPreliminariesAmountText { get; set; }
    public decimal? MainContractorPreliminariesAmount => decimal.TryParse(MainContractorPreliminariesAmountText, out var amount) ? amount : null;
    public string MainContractorPreliminariesDescription { get; set; }

    public string AccessAmountText { get; set; }
    public decimal? AccessAmount => decimal.TryParse(AccessAmountText, out var amount) ? amount : null;
    public string AccessDescription { get; set; }

    public string MainContractorOverheadAmountText { get; set; }
    public decimal? MainContractorOverheadAmount => decimal.TryParse(MainContractorOverheadAmountText, out var amount) ? amount : null;
    public string MainContractorOverheadDescription { get; set; }

    public string ContractorContingenciesAmountText { get; set; }
    public decimal? ContractorContingenciesAmount => decimal.TryParse(ContractorContingenciesAmountText, out var amount) ? amount : null;
    public string ContractorContingenciesDescription { get; set; }
}
