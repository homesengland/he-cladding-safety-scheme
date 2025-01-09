using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class TotalCostsViewModel : WorkPackageBaseViewModel
{
    public decimal? UnsafeCladdingRemovalAmount { get; set; }
    public decimal UnsafeCladdingTotal => UnsafeCladdingRemovalAmount ?? 0;

    public decimal? NewCladdingAmount { get; set; }
    public decimal? ExternalWorksAmount { get; set; }
    public decimal? InternalWorksAmount { get; set; }

    public decimal InstallationTotal =>
        (NewCladdingAmount ?? 0) + (ExternalWorksAmount ?? 0) + (InternalWorksAmount ?? 0);

    public decimal? MainContractorPreliminariesAmount { get; set; }
    public decimal? AccessAmount { get; set; }
    public decimal? MainContractorOverheadAmount { get; set; }
    public decimal? ContractorContingenciesAmount { get; set; }

    public decimal PreliminariesTotal => (MainContractorPreliminariesAmount ?? 0) + (AccessAmount ?? 0) +
                                         (MainContractorOverheadAmount ?? 0) + (ContractorContingenciesAmount ?? 0);

    public decimal? FraewSurveyAmount { get; set; }
    public decimal? FeasibilityStageAmount { get; set; }
    public decimal? PostTenderStageAmount { get; set; }
    public decimal? PropertyManagerAmount { get; set; }
    public decimal? IrrecoverableVatAmount { get; set; }

    public decimal OtherCostsTotal => (FraewSurveyAmount ?? 0) + (FeasibilityStageAmount ?? 0) + (PostTenderStageAmount ?? 0) +
                                      (PropertyManagerAmount ?? 0) + (IrrecoverableVatAmount ?? 0);

    public decimal EligibleTotal => UnsafeCladdingTotal + InstallationTotal + PreliminariesTotal + OtherCostsTotal;

    public decimal? IneligibleAmount { get; set; }
    public decimal OverallTotal => EligibleTotal + (IneligibleAmount ?? 0);
}