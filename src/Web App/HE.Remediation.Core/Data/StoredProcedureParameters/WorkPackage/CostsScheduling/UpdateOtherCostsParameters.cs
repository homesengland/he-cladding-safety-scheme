namespace HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;

public class UpdateOtherCostsParameters
{
    public decimal FeasibilityStageAmount { get; set; }
    public string FeasibilityStageDescription { get; set; }
    public decimal FraewSurveyAmount { get; set; }
    public decimal PostTenderStageAmount { get; set; }
    public string PostTenderStageDescription { get; set; }
    public decimal PropertyManagerAmount { get; set; }
    public string PropertyManagerDescription { get; set; }
    public decimal IrrecoverableVatAmount { get; set; }
    public string IrrecoverableVatDescription{ get; set; }
}