using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.TeamMember;
public class GetTeamMemberCheckYourAnswersRequest : IRequest<GetTeamMemberCheckYourAnswersResponse>
{
    public Guid TeamMemberId { get; set; }
}
