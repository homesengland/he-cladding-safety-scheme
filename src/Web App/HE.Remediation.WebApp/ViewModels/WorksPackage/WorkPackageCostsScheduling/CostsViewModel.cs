using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class CostsViewModel : WorkPackageBaseViewModel
{
    public decimal? UnsafeCladdingRemovalAmount { get; set; }
    public decimal UnsafeCladdingTotal => UnsafeCladdingRemovalAmount ?? 0;

    public decimal? NewCladdingAmount { get; set; }
    public decimal? ExternalWorksAmount { get; set; }
    public decimal? InternalWorksAmount { get; set; }
    public decimal InstallationTotal => (NewCladdingAmount ?? 0) + (ExternalWorksAmount ?? 0) + (InternalWorksAmount ?? 0);

    public decimal? MainContractorPreliminariesAmount { get; set; }
    public decimal? AccessAmount { get; set; }
    public decimal? MainContractorOverheadAmount { get; set; }
    public decimal? ContractorContingenciesAmount { get; set; }

    public decimal PreliminariesTotal => (MainContractorPreliminariesAmount ?? 0) + (AccessAmount ?? 0) +
                                         (MainContractorOverheadAmount ?? 0) + (ContractorContingenciesAmount ?? 0);

    public bool PreliminariesComplete => MainContractorOverheadAmount.HasValue && AccessAmount.HasValue && MainContractorPreliminariesAmount.HasValue && ContractorContingenciesAmount.HasValue;

    public decimal? FraewSurveyAmount { get; set; }
    public decimal? FeasibilityStageAmount { get; set; }
    public decimal? PostTenderStageAmount { get; set; }
    public decimal? PropertyManagerAmount { get; set; }
    public decimal? IrrecoverableVatAmount { get; set; }

    public decimal OtherCostsTotal => (FraewSurveyAmount ?? 0) + (FeasibilityStageAmount ?? 0) + (PostTenderStageAmount ?? 0) +
                                      (PropertyManagerAmount ?? 0) + (IrrecoverableVatAmount ?? 0);

    public bool OtherCostsComplete => FraewSurveyAmount.HasValue && FeasibilityStageAmount.HasValue && PostTenderStageAmount.HasValue &&
                                      PropertyManagerAmount.HasValue && IrrecoverableVatAmount.HasValue;

    public decimal OverallTotal => UnsafeCladdingTotal + InstallationTotal + PreliminariesTotal + OtherCostsTotal;
}
