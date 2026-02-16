using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.ExistingMember.Get;

public class GetExistingMemberRequest : IRequest<GetExistingMemberResponse>
{
    public ETeamRole TeamRole { get; set; }
}