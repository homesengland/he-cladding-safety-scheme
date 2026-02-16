using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AddRole.SetAddRole;

public class SetAddRoleRequest : IRequest<SetAddRoleResponse>
{
    public ETeamRole? TeamRole { get; set; }
}
