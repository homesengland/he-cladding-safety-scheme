using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingSafetyRegulator.Get;

public class GetBuildingControlApprovalRequest : IRequest<GetBuildingControlApprovalResponse>
{
    private GetBuildingControlApprovalRequest()
    {
    }

    public static readonly GetBuildingControlApprovalRequest Request = new();
}