
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.AddRole.Get;

public class GetAddRoleRequest : IRequest<GetAddRoleResponse>
{
    private GetAddRoleRequest()
    {
    }

    public static readonly GetAddRoleRequest Request = new();
}
