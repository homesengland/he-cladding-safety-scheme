using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.TeamMember.Get;

public class GetTeamMemberRequest : IRequest<GetTeamMemberResponse>
{
    public Guid? TeamMemberId { get; set; }
    public ETeamRole TeamRole { get; set; }
    
    public Guid? Selected { get; set; }
}