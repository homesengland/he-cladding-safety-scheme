namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Timescale.Get;

public class GetAdjustEndDateResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
    public int? NewEndMonth { get; set; }
    public int? NewEndYear { get; set; }
    public int? PreviousEndMonth { get; set; }
    public int? PreviousEndYear { get; set; }
    public bool LastMonthlyPaymentCompleted { get; set; }
}