using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.ExistingTeamMember;
public class GetProjectTeamExistingTeamMemberRequest : IRequest<GetProjectTeamExistingTeamMemberResponse>
{
    public ETeamRole TeamRole { get; set; }
    public bool? SameAsPrevious { get; set; }
}
