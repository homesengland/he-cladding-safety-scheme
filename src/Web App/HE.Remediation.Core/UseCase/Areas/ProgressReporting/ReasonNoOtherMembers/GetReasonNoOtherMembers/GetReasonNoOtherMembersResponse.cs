
namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoOtherMembers.GetReasonNoOtherMembers;

public class GetReasonNoOtherMembersResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public string OtherMembersNotAppointedReason { get; set; }

    public bool? OtherMembersNeedsSupport { get; set; }
}
