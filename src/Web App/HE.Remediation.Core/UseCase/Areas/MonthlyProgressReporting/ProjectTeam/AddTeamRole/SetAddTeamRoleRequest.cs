using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.AddTeamRole;

public class SetAddTeamRoleRequest : IRequest<SetAddTeamRoleResponse>
{
    public ETeamRole? TeamRole { get; set; }
}
