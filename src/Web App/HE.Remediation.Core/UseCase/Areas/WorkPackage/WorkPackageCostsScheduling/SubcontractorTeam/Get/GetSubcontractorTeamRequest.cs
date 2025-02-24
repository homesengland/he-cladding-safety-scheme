using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.SubcontractorTeam.Get;

public class GetSubcontractorTeamRequest : IRequest<GetSubcontractorTeamResponse>
{
    private GetSubcontractorTeamRequest()
    {
    }

    public static GetSubcontractorTeamRequest Request => new();
}
