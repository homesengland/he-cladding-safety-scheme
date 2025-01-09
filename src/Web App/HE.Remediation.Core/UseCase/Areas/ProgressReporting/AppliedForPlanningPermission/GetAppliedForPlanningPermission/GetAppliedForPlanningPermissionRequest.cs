using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppliedForPlanningPermission.GetAppliedForPlanningPermission;

public class GetAppliedForPlanningPermissionRequest : IRequest<GetAppliedForPlanningPermissionResponse>
{
    private GetAppliedForPlanningPermissionRequest()
    {
    }

    public static readonly GetAppliedForPlanningPermissionRequest Request = new();
}
