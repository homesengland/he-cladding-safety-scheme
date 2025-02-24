
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AddRole.GetAddRole;

public class GetAddRoleRequest : IRequest<GetAddRoleResponse>
{
    private GetAddRoleRequest()
    {
    }

    public static readonly GetAddRoleRequest Request = new();
}
