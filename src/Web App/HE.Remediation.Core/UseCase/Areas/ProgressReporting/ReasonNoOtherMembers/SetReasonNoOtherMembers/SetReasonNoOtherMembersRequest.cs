using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoOtherMembers.SetReasonNoOtherMembers;

public class SetReasonNoOtherMembersRequest : IRequest
{
    public string OtherMembersNotAppointedReason { get; set; }

    public bool? OtherMembersNeedsSupport { get; set; }
}