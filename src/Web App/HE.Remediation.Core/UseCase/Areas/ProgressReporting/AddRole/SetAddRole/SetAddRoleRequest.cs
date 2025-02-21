using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AddRole.SetAddRole;

public class SetAddRoleRequest : IRequest<SetAddRoleResponse>
{
    public ETeamRole? TeamRole { get; set; }
}
