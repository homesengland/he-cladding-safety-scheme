namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Declaration.Get;

public class GetDeclarationResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool? ConfirmedAccuratelyProfiledCosts { get; set; }
    public bool? ConfirmedAwareOfProcess { get; set; }
    public bool? ConfirmedAwareOfVariationApproval { get; set; }
    public DateTime? ProjectStartDate { get; set; }
    public bool IsSubmitted { get; set; }
}
