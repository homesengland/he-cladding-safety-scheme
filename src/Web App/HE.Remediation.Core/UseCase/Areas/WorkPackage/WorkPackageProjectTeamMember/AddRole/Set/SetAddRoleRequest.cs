using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.AddRole.Set;

public class SetAddRoleRequest : IRequest<SetAddRoleResponse>
{
    public ETeamRole? TeamRole { get; set; }
}
