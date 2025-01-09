namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Other;

public class GetOtherCostsResponse
{
    public decimal? FeasibilityStageAmount { get; set; }
    public string FeasibilityStageDescription { get; set; }
    public decimal? FraewSurveyAmount { get; set; }
    public decimal? PostTenderStageAmount { get; set; }
    public string PostTenderStageDescription { get; set; }
    public decimal? PropertyManagerAmount { get; set; }
    public string PropertyManagerDescription { get; set; }
    public decimal? IrrecoverableVatAmount { get; set; }
    public string IrrecoverableVatDescription { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }
}