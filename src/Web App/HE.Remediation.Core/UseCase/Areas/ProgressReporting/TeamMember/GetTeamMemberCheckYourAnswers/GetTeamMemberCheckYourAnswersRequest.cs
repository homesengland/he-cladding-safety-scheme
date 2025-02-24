using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.GetTeamMemberCheckYourAnswers;

public class GetTeamMemberCheckYourAnswersRequest : IRequest<GetTeamMemberCheckYourAnswersResponse>
{
    public Guid TeamMemberId { get; set; }
}