using HE.Remediation.WebApp.ViewModels.VariationRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class CostsViewModel : VariationRequestBaseViewModel
    {
        public bool VariationCostsValidation { get; set; }

        #region Removal of unsafe cladding

        public decimal? UnsafeCladdingRemovalAmount { get; set; }
        public decimal? VariationUnsafeCladdingRemovalAmount { get; set; }
        public decimal UnsafeCladdingTotal => (UnsafeCladdingRemovalAmount ?? 0) + (VariationUnsafeCladdingRemovalAmount ?? 0);

        #endregion

        #region Installation of new cladding

        public decimal? NewCladdingAmount { get; set; }
        public decimal? ExternalWorksAmount { get; set; }
        public decimal? InternalWorksAmount { get; set; }
        public decimal InstallationTotal => (NewCladdingAmount ?? 0) + (ExternalWorksAmount ?? 0) + (InternalWorksAmount ?? 0);

        public decimal? VariationNewCladdingAmount { get; set; }
        public decimal? VariationExternalWorksAmount { get; set; }
        public decimal? VariationInternalWorksAmount { get; set; }
        public decimal VariationInstallationTotal =>
            (VariationNewCladdingAmount ?? 0) +
            (VariationExternalWorksAmount ?? 0) +
            (VariationInternalWorksAmount ?? 0);

        public decimal? TotalNewCladdingAmount => (NewCladdingAmount ?? 0) + (VariationNewCladdingAmount ?? 0);
        public decimal? TotalExternalWorksAmount => (ExternalWorksAmount ?? 0) + (VariationExternalWorksAmount ?? 0);
        public decimal? TotalInternalWorksAmount => (InternalWorksAmount ?? 0) + (VariationInternalWorksAmount ?? 0);
        public decimal TotalRequestedInternalWorksAmount =>
            (InstallationTotal) +
            (VariationInstallationTotal);

        #endregion

        #region Preliminaries

        public decimal? MainContractorPreliminariesAmount { get; set; }
        public decimal? AccessAmount { get; set; }
        public decimal? MainContractorOverheadAmount { get; set; }
        public decimal? ContractorContingenciesAmount { get; set; }

        public decimal PreliminariesTotal =>
            (MainContractorPreliminariesAmount ?? 0) +
            (AccessAmount ?? 0) +
            (MainContractorOverheadAmount ?? 0) +
            (ContractorContingenciesAmount ?? 0);

        //public bool PreliminariesComplete => MainContractorOverheadAmount.HasValue && AccessAmount.HasValue && MainContractorPreliminariesAmount.HasValue && ContractorContingenciesAmount.HasValue;

        public decimal? VariationMainContractorPreliminariesAmount { get; set; }
        public decimal? VariationAccessAmount { get; set; }
        public decimal? VariationMainContractorOverheadAmount { get; set; }
        public decimal? VariationContractorContingenciesAmount { get; set; }

        public decimal VariationPreliminariesTotal =>
            (VariationMainContractorPreliminariesAmount ?? 0) +
            (VariationAccessAmount ?? 0) +
            (VariationMainContractorOverheadAmount ?? 0) +
            (VariationContractorContingenciesAmount ?? 0);

        public decimal? TotalMainContractorPreliminariesAmount => (MainContractorPreliminariesAmount ?? 0) + (VariationMainContractorPreliminariesAmount ?? 0);
        public decimal? TotalAccessAmount => (AccessAmount ?? 0) + (VariationAccessAmount ?? 0);
        public decimal? TotalMainContractorOverheadAmount => (MainContractorOverheadAmount ?? 0) + (VariationMainContractorOverheadAmount ?? 0);
        public decimal? TotalContractorContingenciesAmount => (ContractorContingenciesAmount ?? 0) + (VariationContractorContingenciesAmount ?? 0);

        public decimal TotalRequestedPreliminariesTotal =>
            (PreliminariesTotal) +
            (VariationPreliminariesTotal);

        #endregion

        #region Other Costs

        public decimal? FraewSurveyAmount { get; set; }
        public decimal? FeasibilityStageAmount { get; set; }
        public decimal? PostTenderStageAmount { get; set; }
        public decimal? PropertyManagerAmount { get; set; }
        public decimal? IrrecoverableVatAmount { get; set; }

        public decimal OtherCostsTotal => (FraewSurveyAmount ?? 0) + (FeasibilityStageAmount ?? 0) + (PostTenderStageAmount ?? 0) +
                                         (PropertyManagerAmount ?? 0) + (IrrecoverableVatAmount ?? 0);

        //public bool OtherCostsComplete => FraewSurveyAmount.HasValue && FeasibilityStageAmount.HasValue && PostTenderStageAmount.HasValue &&
        //                  PropertyManagerAmount.HasValue && IrrecoverableVatAmount.HasValue;


        public decimal? VariationFraewSurveyAmount { get; set; }
        public decimal? VariationFeasibilityStageAmount { get; set; }
        public decimal? VariationPostTenderStageAmount { get; set; }
        public decimal? VariationPropertyManagerAmount { get; set; }
        public decimal? VariationIrrecoverableVatAmount { get; set; }

        public decimal VariationOtherCostsTotal =>
            (VariationFraewSurveyAmount ?? 0) +
            (VariationFeasibilityStageAmount ?? 0) +
            (VariationPostTenderStageAmount ?? 0) +
            (VariationPropertyManagerAmount ?? 0) +
            (VariationIrrecoverableVatAmount ?? 0);

        public decimal? TotalFraewSurveyAmount => (FraewSurveyAmount ?? 0) + (VariationFraewSurveyAmount ?? 0);
        public decimal? TotalFeasibilityStageAmount => (FeasibilityStageAmount ?? 0) + (VariationFeasibilityStageAmount ?? 0);
        public decimal? TotalPostTenderStageAmount => (PostTenderStageAmount ?? 0) + (VariationPostTenderStageAmount ?? 0);
        public decimal? TotalPropertyManagerAmount => (PropertyManagerAmount ?? 0) + (VariationPropertyManagerAmount ?? 0);
        public decimal? TotalIrrecoverableVatAmount => (IrrecoverableVatAmount ?? 0) + (VariationIrrecoverableVatAmount ?? 0);

        public decimal RequestedTotalOtherCosts =>
            (OtherCostsTotal) +
            (VariationOtherCostsTotal);

        #endregion

        #region Totals

        public decimal OverallTotal =>
            (UnsafeCladdingRemovalAmount ?? 0) +
            InstallationTotal +
            PreliminariesTotal +
            OtherCostsTotal;

        public decimal VariationOverallTotal =>
            (VariationUnsafeCladdingRemovalAmount ?? 0) +
            VariationInstallationTotal +
            VariationPreliminariesTotal +
            VariationOtherCostsTotal;

        public decimal RequestedOverallTotal =>
            OverallTotal +
            VariationOverallTotal;

        #endregion
    }
}