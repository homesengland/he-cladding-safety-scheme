using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class CostsDescriptionViewModel : WorkPackageBaseViewModel
{
    public string UnsafeCladdingRemovalDescription { get; set; }
    
    public string NewCladdingDescription { get; set; }
    public string ExternalWorksDescription { get; set; }
    public string InternalWorksDescription { get; set; }
    
    public string MainContractorPreliminariesDescription { get; set; }
    public string AccessDescription { get; set; }
    public string MainContractorOverheadDescription { get; set; }
    public string ContractorContingenciesDescription { get; set; }

    public decimal FraewSurveyDescription { get; set; }
    public string FeasibilityStageDescription { get; set; }
    public string PostTenderStageDescription { get; set; }
    public string PropertyManagerDescription { get; set; }
    public string IrrecoverableVatDescription { get; set; }
}