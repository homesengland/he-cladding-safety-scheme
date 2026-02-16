using Mediator;
namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ApprovalDateGateWayTwoApplication.Set;

public class SetApprovalDateRequest : IRequest
{
    public int? ApprovalDateDay { get; set; }
    public int? ApprovalDateMonth { get; set; }
    public int? ApprovalDateYear { get; set; }
}