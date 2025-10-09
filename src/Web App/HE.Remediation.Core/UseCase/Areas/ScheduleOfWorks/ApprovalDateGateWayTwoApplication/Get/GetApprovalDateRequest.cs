using MediatR;
namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ApprovalDateGateWayTwoApplication.Get;

public class GetApprovalDateRequest : IRequest<GetApprovalDateResponse>
{
    private GetApprovalDateRequest()
    {
    }

    public static readonly GetApprovalDateRequest Request = new();
}