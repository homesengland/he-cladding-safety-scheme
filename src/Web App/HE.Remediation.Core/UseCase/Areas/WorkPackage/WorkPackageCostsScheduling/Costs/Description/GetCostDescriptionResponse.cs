namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Description;

public class GetCostDescriptionResponse
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

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }
}