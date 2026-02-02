using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedOtherMembers.SetAppointedOtherMembers;

public class SetAppointedOtherMembersRequest : IRequest
{
    public bool? OtherMembersAppointed { get; set; }
}
