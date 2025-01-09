using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ExistingTeamMember;

public class GetExistingTeamMemberRequest : IRequest<GetExistingTeamMemberResponse>
{
    public ETeamRole TeamRole { get; set; }
}