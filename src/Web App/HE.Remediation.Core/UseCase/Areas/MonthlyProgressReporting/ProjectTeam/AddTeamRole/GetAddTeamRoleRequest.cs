using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.AddTeamRole;

public class GetAddTeamRoleRequest : IRequest<GetAddTeamRoleResponse>
{
    private GetAddTeamRoleRequest()
    {
    }

    public static readonly GetAddTeamRoleRequest Request = new();
}
