using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetProjectTeam;

public class GetProjectTeamRequest : IRequest<GetProjectTeamResponse>
{
    private GetProjectTeamRequest()
    {
    }

    public static readonly GetProjectTeamRequest Request = new();
}
