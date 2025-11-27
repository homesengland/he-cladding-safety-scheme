using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.NoTeam;
public class GetProjectTeamNoTeamRequest : IRequest<GetProjectTeamNoTeamResponse>
{
    private GetProjectTeamNoTeamRequest()
    {
    }
    public static readonly GetProjectTeamNoTeamRequest Request = new();
}
