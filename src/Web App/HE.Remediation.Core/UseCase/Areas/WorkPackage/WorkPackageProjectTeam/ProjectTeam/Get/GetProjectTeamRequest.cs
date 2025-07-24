using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.ProjectTeam.Get;

public class GetProjectTeamRequest : IRequest<GetProjectTeamResponse>
{
    private GetProjectTeamRequest()
    {
    }

    public static readonly GetProjectTeamRequest Request = new();
}
