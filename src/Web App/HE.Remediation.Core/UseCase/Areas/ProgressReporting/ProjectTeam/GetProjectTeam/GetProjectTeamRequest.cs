
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectTeam.GetProjectTeam;

public class GetProjectTeamRequest : IRequest<GetProjectTeamResponse>
{
    private GetProjectTeamRequest()
    {
    }

    public static readonly GetProjectTeamRequest Request = new();
}
