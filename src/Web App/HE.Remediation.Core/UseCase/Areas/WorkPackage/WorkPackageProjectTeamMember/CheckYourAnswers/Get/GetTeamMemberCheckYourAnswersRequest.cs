using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.CheckYourAnswers.Get;

public class GetTeamMemberCheckYourAnswersRequest : IRequest<GetTeamMemberCheckYourAnswersResponse>
{
    public Guid TeamMemberId { get; set; }
}