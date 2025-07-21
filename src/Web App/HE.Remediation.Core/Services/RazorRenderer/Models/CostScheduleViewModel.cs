
namespace HE.Remediation.Core.Services.RazorRenderer.Models
{
    public class CostScheduleViewModel
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }

        public decimal? UnsafeCladdingRemovalAmount { get; set; }
        public string UnsafeCladdingRemovalDescription { get; set; }
        public decimal UnsafeCladdingTotal => UnsafeCladdingRemovalAmount ?? 0;

        public decimal? NewCladdingAmount { get; set; }
        public string NewCladdingDescription { get; set; }
        public decimal? ExternalWorksAmount { get; set; }
        public string ExternalWorksDescription { get; set; }
        public decimal? InternalWorksAmount { get; set; }
        public string InternalWorksDescription { get; set; }
        public decimal InstallationTotal => (NewCladdingAmount ?? 0) + (ExternalWorksAmount ?? 0) + (InternalWorksAmount ?? 0);

        public decimal? MainContractorPreliminariesAmount { get; set; }
        public string MainContractorPreliminariesDescription { get; set; }
        public decimal? AccessAmount { get; set; }
        public string AccessDescription { get; set; }
        public decimal? MainContractorOverheadAmount { get; set; }
        public string MainContractorOverheadDescription { get; set; }
        public decimal? ContractorContingenciesAmount { get; set; }
        public string ContractorContingenciesDescription { get; set; }

        public decimal PreliminariesTotal => (MainContractorPreliminariesAmount ?? 0) + (AccessAmount ?? 0) +
                                             (MainContractorOverheadAmount ?? 0) + (ContractorContingenciesAmount ?? 0);

        public decimal? FraewSurveyAmount { get; set; }
        public decimal? FeasibilityStageAmount { get; set; }
        public string FeasibilityStageDescription { get; set; }
        public decimal? PostTenderStageAmount { get; set; }
        public string PostTenderStageDescription { get; set; }
        public decimal? PropertyManagerAmount { get; set; }
        public string PropertyManagerDescription { get; set; }
        public decimal? IrrecoverableVatAmount { get; set; }
        public string IrrecoverableVatDescription { get; set; }
        public decimal? IneligibleAmount { get; set; }

        public decimal OtherCostsTotal => (FraewSurveyAmount ?? 0) + (FeasibilityStageAmount ?? 0) + (PostTenderStageAmount ?? 0) +
                                          (PropertyManagerAmount ?? 0) + (IrrecoverableVatAmount ?? 0);

        public decimal OverallTotal => UnsafeCladdingTotal + InstallationTotal + PreliminariesTotal + OtherCostsTotal;
    }
}
