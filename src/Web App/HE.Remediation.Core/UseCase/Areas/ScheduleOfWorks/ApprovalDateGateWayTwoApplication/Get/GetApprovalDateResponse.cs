namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ApprovalDateGateWayTwoApplication.Get;

public class GetApprovalDateResponse
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public bool IsSubmitted { get; set; }
    public int? ApprovalDateDay { get; set; }
    public int? ApprovalDateMonth { get; set; }
    public int? ApprovalDateYear { get; set; }
}